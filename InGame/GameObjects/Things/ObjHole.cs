using Microsoft.Xna.Framework;
using ProjectZ.InGame.GameObjects.Base;
using ProjectZ.InGame.GameObjects.Base.CObjects;
using ProjectZ.InGame.GameObjects.Base.Components;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.GameObjects.Things
{
    internal class ObjHole : GameObject
    {
        private readonly DrawSpriteComponent _drawComponent;
        private readonly BoxCollisionComponent _collisionComponent;

        public readonly Vector2 Center;
        public readonly int Color;

        public ObjHole() : base("hole_0") { }

        public ObjHole(Map.Map map, int posX, int posY, int width, int height, Rectangle sourceRectangle, int offsetX, int offsetY, int color) : base(map)
        {
            Tags = Values.GameObjectTag.Hole;

            Center = new Vector2(posX + offsetX + width / 2, posY + offsetY + height / 2);
            Color = color;

            if (sourceRectangle == Rectangle.Empty)
            {
                EntityPosition = new CPosition(posX + offsetX, posY + offsetY, 0);
                EntitySize = new Rectangle(0, 0, width, height);
            }
            else
            {
                EntityPosition = new CPosition(posX, posY, 0);
                EntitySize = new Rectangle(0, 0, sourceRectangle.Width, sourceRectangle.Height);
            }

            // HACK: We want the collision with the hole to be slightly smaller than the actual hole since Link's body will intersect with
            // the edges as soon as he cross the boundary of the sprite. This limits the distance at which it starts pulling, which matches
            // the behavior of the original game. The modifications below make hole collision size 14x8 pixels instead of 16x16 pixels.
            float rectOffsetX = (width == 16 && height == 16) ? posX + offsetX + 1 : posX + offsetX;
            float rectOffsetY = (width == 16 && height == 16) ? posY + offsetY + 4 : posY + offsetY;
            float rectWidth   = (width == 16 && height == 16) ? width - 2          : width;
            float rectHeight  = (width == 16 && height == 16) ? height - 8         : height;

            _collisionComponent = new BoxCollisionComponent(new CBox(rectOffsetX, rectOffsetY, 0, rectWidth, rectHeight, 16), Values.CollisionTypes.Hole);
            AddComponent(CollisionComponent.Index, _collisionComponent);

            // visible hole?
            if (sourceRectangle != Rectangle.Empty)
            {
                _drawComponent = new DrawSpriteComponent(Resources.SprObjects, EntityPosition, sourceRectangle, new Vector2(0, 0), Values.LayerBottom);
                AddComponent(DrawComponent.Index, _drawComponent);
            }
        }

        public void SetActive(bool state)
        {
            if (_drawComponent != null)
                _drawComponent.IsActive = state;
            _collisionComponent.IsActive = state;
        }
    }
}