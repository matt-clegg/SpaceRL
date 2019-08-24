using Monocle;
using SpaceRL.Core;
using System;

namespace SpaceRL.DirectX
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += Engine.OnUnhandledException;

            const int width = 640;
            const int height = 360;
            const int windowWidth = width * 2;
            const int windowHeight = height * 2;
            const string title = "SpaceRL";
            const bool fullscreen = false;

            using (MainGame game = new MainGame(width, height, windowWidth, windowHeight, title, fullscreen))
            {
                game.Run();
            }
        }
    }
#endif
}
