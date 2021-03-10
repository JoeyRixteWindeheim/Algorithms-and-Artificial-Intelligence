using System;

namespace AAIFinalAssignment
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
            {

                game.Window.AllowUserResizing = true;
                game.Run();
            }
        }
    }
}
