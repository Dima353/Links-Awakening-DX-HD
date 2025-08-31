using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectZ.InGame.GameObjects.Base;
using ProjectZ.InGame.GameObjects.Base.CObjects;
using ProjectZ.InGame.GameObjects.Base.Components;
using ProjectZ.InGame.SaveLoad;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.GameObjects.Dungeon
{
    internal class ObjDungeonBarrier : GameObject
    {
        private readonly DrawComponent _drawComponent;
        private readonly List<GameObject> _collidingObjects = new List<GameObject>();

        private readonly DictAtlasEntry _dictBarrier;
        private readonly DictAtlasEntry _dictBarrierBack;

        private readonly CBox _bodyBox;

        private readonly string _key;
        private readonly bool _negate;

        private const int StateTimer = 200;
        private float _stateCounter;
        private float _transitionPercentage;
        private float _transitionState;

        private bool _isUp;

        public ObjDungeonBarrier(Map.Map map, int posX, int posY, string strKey, bool negate, int type) : base(map)
        {
            type = MathHelper.Clamp(type, 0, 3);

            _dictBarrier = Resources.GetSprite("barrier_" + type);
            _dictBarrierBack = Resources.GetSprite("barrier_bottom_" + type);

            SprEditorImage = _dictBarrier.Texture;
            EditorIconSource = _dictBarrier.ScaledRectangle;
            EditorIconScale = _dictBarrier.Scale;

            EntityPosition = new CPosition(posX + 2.5f, posY + 5, 0);
            EntitySize = new Rectangle(0, -5, 11, 14);

            _key = strKey;
            _negate = negate;

            var collisionComponent = new BoxCollisionComponent(
                _bodyBox = new CBox(EntityPosition, 0, -1, 11, 8, 4), Values.CollisionTypes.Normal);

            AddComponent(CollisionComponent.Index, collisionComponent);
            AddComponent(KeyChangeListenerComponent.Index, new KeyChangeListenerComponent(KeyChanged));
            AddComponent(UpdateComponent.Index, new UpdateComponent(Update));
            AddComponent(DrawShadowComponent.Index, new DrawShadowComponent(DrawShadow));
            AddComponent(DrawComponent.Index, _drawComponent = new DrawComponent(Draw, Values.LayerBottom, EntityPosition));

            KeyChanged();
            if (_isUp)
                _stateCounter = StateTimer;
        }

        private void KeyChanged()
        {
            if (!string.IsNullOrWhiteSpace(_key))
            {
                _isUp = _negate != (Game1.GameManager.SaveManager.GetString(_key, "0") == "1");
                if (_isUp)
                    _drawComponent.Layer = Values.LayerPlayer;
            }
        }

        private void Update()
        {
            if (!_isUp && _stateCounter > 0)
            {
                _stateCounter -= Game1.DeltaTime;

                if (_stateCounter < 0)
                {
                    _stateCounter = 0;
                    _drawComponent.Layer = Values.LayerBottom;
                }
            }
            else if (_isUp && _stateCounter < StateTimer)
            {
                _stateCounter += Game1.DeltaTime;

                if (_stateCounter > StateTimer)
                    _stateCounter = StateTimer;
            }

            _transitionPercentage = MathF.Sin((_stateCounter / StateTimer) * MathF.PI - MathF.PI / 2) * 0.5f + 0.5f;
            _transitionState = _transitionPercentage * 4;

            if (EntityPosition.Z != _transitionState - 4)
            {
                var lastBox = _bodyBox.Box;

                EntityPosition.Z = _transitionState - 4;
                EntityPosition.NotifyListeners();

                // check for colliding bodies and push them forward
                _collidingObjects.Clear();
                Map.Objects.GetComponentList(_collidingObjects,
                    (int)EntityPosition.Position.X, (int)EntityPosition.Position.Y - 1, 11, 8, BodyComponent.Mask);

                foreach (var collidingObject in _collidingObjects)
                {
                    var body = (BodyComponent)collidingObject.Components[BodyComponent.Index];

                    if (body.BodyBox.Box.Intersects(_bodyBox.Box))
                    {
                        if (!body.BodyBox.Box.Intersects(lastBox))
                        {
                            body.Position.Z = EntityPosition.Z + _bodyBox.Box.Depth;
                            body.Position.NotifyListeners();
                        }
                    }
                    // HACK: This is a gross, gross hack that will detect if link is jumping while the blocks are changing to the upward
                    // state. A jump will want to return him to "0" Z-position which will cause him to get stuck into the blocks unable to
                    // move. The timer detects if a jump was performed at 0-3.99 elevation and will set him to "4" Z-Position when he lands.
                    // A proper fix should go into: GameObjects > Base > Systems > SystemBody >>> "UpdateVelocityZ" but it's beyond me.

                    if (collidingObject.GetType() == typeof(ObjLink))
                    {
                        // Get the colliding object if it's link.
                        ObjLink Link = (ObjLink)collidingObject;

                        // Check if Link was jumping and his jumping off point was less than 4 on Z-Axis.
                        if (Link.CurrentState == ObjLink.State.Jumping || 
                            Link.CurrentState == ObjLink.State.AttackJumping  || 
                            Link.CurrentState == ObjLink.State.ChargeJumping && Link._jumpStartZPos < 4.00)
                        {
                            // Run a timer to track when the jump has ended so his Z-Position can be "fixed".
                            Timer _jumpFixHackTimer = new Timer(100);
                            _jumpFixHackTimer.Elapsed += (s,e) => JumpFixHack(Link,_bodyBox,s,e);
                            _jumpFixHackTimer.AutoReset = true;
                            _jumpFixHackTimer.Enabled = true;
                        }
                    }
                }
            }
        }

        private static void JumpFixHack(ObjLink Link, CBox TrapBody, object sender, ElapsedEventArgs e)
        {
            // This is the part of the gross, gross hack that fixes Link's Z-Position if he jumped while blocks were going into the upward state.
            Timer jumpFixHackTimer = (sender as Timer);

            // Detect when the jump has ended.
            if (Link._body.IsGrounded &&
                Link.CurrentState != ObjLink.State.Jumping && 
                Link.CurrentState != ObjLink.State.Attacking && 
                Link.CurrentState != ObjLink.State.AttackJumping && 
                Link.CurrentState != ObjLink.State.Powdering && 
                Link.CurrentState != ObjLink.State.Pushing && 
                Link.CurrentState != ObjLink.State.Charging &&
                Link.CurrentState != ObjLink.State.ChargeJumping ||
                Link.CurrentState == ObjLink.State.Idle )
            {
                // If he's still over the boxes, then fix his Z-Position.
                if (Link._body.BodyBox.Box.Intersects(TrapBody.Box))
                    Link._body.Position.Z = 4;

                jumpFixHackTimer.Stop();
                jumpFixHackTimer.Dispose();
            }
        }

        private void Draw(SpriteBatch spriteBatch)
        {
            // draw the bottom part
            DrawHelper.DrawNormalized(spriteBatch, _dictBarrierBack, new Vector2(EntityPosition.X, EntityPosition.Y - 1), Color.White);

            // draw the barrier
            if (_transitionState != 0)
            {
                var rectangle = _dictBarrier.ScaledRectangle;
                rectangle.Height = (int)((_dictBarrier.SourceRectangle.Height - 4 + _transitionState) / _dictBarrier.Scale);
                DrawHelper.DrawNormalized(spriteBatch, _dictBarrier.Texture,
                    new Vector2(EntityPosition.X, EntityPosition.Y - 1 - _transitionState), rectangle, Color.White);
            }
        }

        private void DrawShadow(SpriteBatch spriteBatch)
        {
            DrawHelper.DrawShadow(_dictBarrier.Texture, new Vector2(EntityPosition.X, EntityPosition.Y - 6),
                _dictBarrier.ScaledRectangle, _dictBarrier.SourceRectangle.Width, _dictBarrier.SourceRectangle.Height, false,
                Map.ShadowHeight, Map.ShadowRotation, Color.White * _transitionPercentage);
        }
    }
}