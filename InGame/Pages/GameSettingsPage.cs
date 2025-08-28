using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ProjectZ.InGame.Controls;
using ProjectZ.InGame.Interface;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.Pages
{
    class GameSettingsPage : InterfacePage
    {
        private readonly InterfaceListLayout _bottomBar;
        private readonly InterfaceButton _controllerType;

        public GameSettingsPage(int width, int height)
        {
            // graphic settings layout
            var gameSettingsList = new InterfaceListLayout { Size = new Point(width, height), Selectable = true };
            var buttonWidth = 320;

            gameSettingsList.AddElement(new InterfaceLabel(Resources.GameHeaderFont, "settings_game_header",
                new Point(buttonWidth, (int)(height * Values.MenuHeaderSize)), new Point(0, 0)));

            var contentLayout = new InterfaceListLayout { Size = new Point(width, (int)(height * Values.MenuContentSize)), Selectable = true, ContentAlignment = InterfaceElement.Gravities.Top };

            // Language Button:
            contentLayout.AddElement(new InterfaceButton(new Point(buttonWidth, 18), new Point(0, 2), "settings_game_language", PressButtonLanguageChange));

            // Controller Type Button:
            // There wasn't a way to just display what we want on the button so a little bit of hackery was needed.
            contentLayout.AddElement(_controllerType = new InterfaceButton(new Point(buttonWidth, 18), new Point(0, 2), "", PressButtonSetController)) ;
            _controllerType.InsideLabel.OverrideText = Game1.LanguageManager.GetString("settings_game_controller", "error") + ":" + GameSettings.Controller;

            // AutoSave Toggle:
            var toggleAutosave = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_game_autosave", GameSettings.Autosave, newState => { GameSettings.Autosave = newState; });
            contentLayout.AddElement(toggleAutosave);

            // Items on Right Toggle:
            var toggleItemSlotSide = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_game_items_on_right", GameSettings.ItemsOnRight, newState => { GameSettings.ItemsOnRight = newState; });
            contentLayout.AddElement(toggleItemSlotSide);

            var toggleNoPickupDialogs = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_modifier_no_pickup_dialogs", GameSettings.NoPickupDialogs, value => { GameSettings.NoPickupDialogs = value; Game1.GameManager.ItemManager.Load(); });
            contentLayout.AddElement(toggleNoPickupDialogs);

            // Screen-Shake Toggle:
            var toggleScreenShake = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_game_screenshake", GameSettings.ScreenShake, newState => { GameSettings.ScreenShake = newState; });
            contentLayout.AddElement(toggleScreenShake);

            gameSettingsList.AddElement(contentLayout);

            _bottomBar = new InterfaceListLayout() { Size = new Point(width, (int)(height * Values.MenuFooterSize)), Selectable = true, HorizontalMode = true };
            // back button
            _bottomBar.AddElement(new InterfaceButton(new Point(100, 20), new Point(2, 4), "settings_menu_back", element =>
            {
                Game1.UiPageManager.PopPage();
            }));

            gameSettingsList.AddElement(_bottomBar);

            PageLayout = gameSettingsList;
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
            // the left button is always the first one selected
            _bottomBar.Deselect(false);
            _bottomBar.Select(InterfaceElement.Directions.Left, false);
            _bottomBar.Deselect(false);

            PageLayout.Deselect(false);
            PageLayout.Select(InterfaceElement.Directions.Top, false);
        }

        public void PressButtonLanguageChange(InterfaceElement element)
        {
            Game1.LanguageManager.ToggleLanguage();

            // Because of the hacky way that text is imposed on the Controller button, we need to manually update the language change.
            _controllerType.InsideLabel.OverrideText = Game1.LanguageManager.GetString("settings_game_controller", "error") + ":" + GameSettings.Controller;
        }

        public void PressButtonSetController(InterfaceElement element)
        {
            // Push forward the index +1 and loop back around.
            int index = Array.IndexOf(ControlHandler.ControllerNames, GameSettings.Controller);
            index = (index + 1) % ControlHandler.ControllerNames.Length;
            GameSettings.Controller = ControlHandler.ControllerNames[index];
            ControlHandler.SetControllerIndex();

            // Override the button text with this fancy hack.
            _controllerType.InsideLabel.OverrideText = Game1.LanguageManager.GetString("settings_game_controller", "error") + ":" + GameSettings.Controller;

            // Update the buttons on the controller page.
            ControlSettingsPage.UpdateLabels();
        }
    }
}
