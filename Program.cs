using System;
using ProjectZ.InGame.Things;

namespace ProjectZ
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var editorMode = false;
            var loadFirstSave = false;

            foreach (var arg in args)
            {
                if (arg == "editor")
                    editorMode = true;
                else if (arg == "loadSave")
                    loadFirstSave = true;
                else if (arg == "exclusive")
                    GameSettings.ExFullscreen = true;
            }

            try
            {
                using (var game = new Game1(editorMode, loadFirstSave))
                    game.Run();
            }
            catch (Exception exception)
            {
#if WINDOWS
                System.Windows.Forms.MessageBox.Show(
                    exception.StackTrace,
                    exception.Message,
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
#else
                Console.WriteLine("=== Unhandled Exception ===");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
#endif
                throw;
            }
        }
    }
}