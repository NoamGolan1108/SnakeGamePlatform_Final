using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGamePlatform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
         {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Board b = new Board();
            Application.Run(b.GetBoard());
            
        }
        public static void Game()
        {
            Board b = new Board();
            Application.Run(b.GetBoard());
        }
    }
}
