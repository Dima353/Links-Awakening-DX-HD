using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using ProjectZ.InGame.Controls;
using ProjectZ.InGame.Interface;
using ProjectZ.InGame.Things;

namespace ProjectZ.InGame.Pages
{
    class GraphicSettingsPage : InterfacePage
    {
        private readonly InterfaceSlider _uiScaleSlider;
        private readonly InterfaceSlider _gameScaleSlider;
        private readonly InterfaceListLayout _bottomBar;
        private readonly InterfaceListLayout _toggleFullscreen;

        public GraphicSettingsPage(int width, int height)
        {
            // Graphics settings layout.
            var _graphicSettingsLayout = new InterfaceListLayout { Size = new Point(width, height), Selectable = true };
            var buttonWidth = 240;
            _graphicSettingsLayout.AddElement(new InterfaceLabel(Resources.GameHeaderFont, "settings_graphics_header",
                new Point(buttonWidth, (int)(height * Values.MenuHeaderSize)), new Point(0, 0)));
            var contentLayout = new InterfaceListLayout { Size = new Point(width, (int)(height * Values.MenuContentSize)), Selectable = true, ContentAlignment = InterfaceElement.Gravities.Top };

            // Slider to adjust the game scale.
            _gameScaleSlider = new InterfaceSlider(Resources.GameFont, "settings_graphics_game_scale",
                buttonWidth, new Point(1, 2), -1, 11, 1, GameSettings.GameScale + 1,
                number =>
                {
                    GameSettings.GameScale = number;
                    Game1.ScaleChanged = true;
                })
            { SetString = number => GameScaleSliderAdjustmentString(number) };
            contentLayout.AddElement(_gameScaleSlider);

            // Saved value may be larger than current size.
            if (GameSettings.UiScale > Game1.ScreenScale)
                GameSettings.UiScale = Game1.ScreenScale;

            // Slider to adjust the user interface.
            _uiScaleSlider = new InterfaceSlider(Resources.GameFont, "settings_graphics_ui_scale",
                buttonWidth, new Point(1, 2), 1, Game1.ScreenScale, 1, GameSettings.UiScale - 1,
                number =>
                {
                    GameSettings.UiScale = number;
                    Game1.ScaleChanged = true;
                })
            { SetString = number => UIScaleSliderAdjustmentString(number) };
            contentLayout.AddElement(_uiScaleSlider);

            // Fullscreen toggler.
            _toggleFullscreen = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_game_fullscreen_mode", GameSettings.IsFullscreen,
                newState => {
                    Game1.ToggleFullscreen();
                    Game1.ScaleChanged = true;
                });
            contentLayout.AddElement(_toggleFullscreen);

            // Shadow toggler.
            // TODO: Also disables shadows under the player sprite. At least this shadow should be drawn.
            var shadowToggle = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
               "settings_graphics_shadow", GameSettings.EnableShadows, newState => GameSettings.EnableShadows = newState);
             contentLayout.AddElement(shadowToggle);

            // FPS lock toggler.
            var toggleFpsLock = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_graphics_fps_lock", GameSettings.LockFps, newState =>
                {
                    GameSettings.LockFps = newState;
                    Game1.FpsSettingChanged = true;
                });
            contentLayout.AddElement(toggleFpsLock);

            // Smooth camera toggler.
            var smoothCameraToggle = InterfaceToggle.GetToggleButton(new Point(buttonWidth, 18), new Point(5, 2),
                "settings_game_change_smooth_camera", GameSettings.SmoothCamera, newState => { GameSettings.SmoothCamera = newState; });
            contentLayout.AddElement(smoothCameraToggle);
            _graphicSettingsLayout.AddElement(contentLayout);

            _bottomBar = new InterfaceListLayout { Size = new Point(width, (int)(height * Values.MenuFooterSize)), Selectable = true, HorizontalMode = true };

            // Back button.
            _bottomBar.AddElement(new InterfaceButton(new Point(60, 20), new Point(2, 4), "settings_menu_back", element =>
            {
                Game1.UiPageManager.PopPage();
            }));

            _graphicSettingsLayout.AddElement(_bottomBar);
            PageLayout = _graphicSettingsLayout;
        }

        public override void Update(CButtons pressedButtons, GameTime gameTime)
        {
            base.Update(pressedButtons, gameTime);

            UpdateFullscreenState();
            UpdateGameScaleSlider();
            UpdateUIScaleSlider();

            // close the page
            if (ControlHandler.ButtonPressed(CButtons.B))
                Game1.UiPageManager.PopPage();
        }

        private string GameScaleSliderAdjustmentString(int number)
        {   
            string value = ((GameSettings.GameScale == 11) 
                ? "Auto-Detect" 
                : " x" + ((number < 1) 
                    ? "1/" + (2 - number) 
                    : number.ToString()));
            return value;
        }

        private string UIScaleSliderAdjustmentString(int number)
        {   
            string value = (number == Game1.ScreenScale)
                ? "Auto-Detect" 
                : " x" + number;
            return value;
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

        public override void OnResize(int newWidth, int newHeight)
        {
            UpdateUIScaleSlider();
        }
        private void UpdateFullscreenState()
        {
            var toggle = ((InterfaceToggle)_toggleFullscreen.Elements[1]);
            if (toggle.ToggleState != GameSettings.IsFullscreen)
                toggle.SetToggle(GameSettings.IsFullscreen);
        }

        private void UpdateGameScaleSlider()
        {
            var currentScale = GameSettings.GameScale;
            _gameScaleSlider.CurrentStep = currentScale + 1;
        }

        private void UpdateUIScaleSlider()
        {
            _uiScaleSlider.UpdateStepSize(1, Game1.ScreenScale, 1);
            _uiScaleSlider.CurrentStep = GameSettings.UiScale - 1;
        }
    }
}
