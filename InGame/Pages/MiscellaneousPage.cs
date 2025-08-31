using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectZ.InGame.Controls;
using ProjectZ.InGame.Interface;
using ProjectZ.InGame.Screens;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.Pages
{
    class MiscellaneousPage : InterfacePage
    {
        private readonly InterfaceListLayout _bottomBar;

        public MiscellaneousPage(int width, int height, ContentManager content)
        {
            // graphic settings layout
            var miscellaneousList = new InterfaceListLayout { Size = new Point(width, height), Selectable = true };
            var buttonWidth = 320;
            miscellaneousList.AddElement(new InterfaceLabel(Resources.GameHeaderFont, "settings_misc_header",
                new Point(buttonWidth, (int)(height * Values.MenuHeaderSize)), new Point(0, 0)));
            var contentLayout = new InterfaceListLayout { Size = new Point(width, (int)(height * Values.MenuContentSize)), Selectable = true, ContentAlignment = InterfaceElement.Gravities.Top };

            // Original Menu Border:
            var toggleMenuBricks = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_misc_menubricks", GameSettings.OldMenuBorder, newState => 
                { 
                    GameSettings.OldMenuBorder = newState;

                    var texture = newState
                        ? content.Load<Texture2D>("Menu/menuBackgroundAlt")
                        : content.Load<Texture2D>("Menu/menuBackground");

                    var menuScreen = (MenuScreen)Game1.ScreenManager.GetScreen(Values.ScreenNameMenu);
                    menuScreen?.SetBackground(texture);
                });
            contentLayout.AddElement(toggleMenuBricks);

            _bottomBar = new InterfaceListLayout() { Size = new Point(width, (int)(height * Values.MenuFooterSize)), Selectable = true, HorizontalMode = true };

            // back button
            _bottomBar.AddElement(new InterfaceButton(new Point(100, 20), new Point(2, 4), "settings_menu_back", element =>
            {
                Game1.UiPageManager.PopPage();
            }));

            miscellaneousList.AddElement(contentLayout);
            miscellaneousList.AddElement(_bottomBar);

            PageLayout = miscellaneousList;
        }

        public override void Update(CButtons pressedButtons, GameTime gameTime)
        {
            base.Update(pressedButtons, gameTime);

            // close the page
            if (ControlHandler.ButtonPressed(CButtons.B))
                Game1.UiPageManager.PopPage();
        }

        public override void OnLoad(Dictionary<string, object> intent)
        {
            _bottomBar.Deselect(false);
            PageLayout.Deselect(false);
            PageLayout.Select(InterfaceElement.Directions.Top, false);
        }
    }
}
