using System;
using System.Windows.Forms;

namespace TowerDefenseColab
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
            using (GameWindow gw = new GameWindow())
            {
                gw.GameLoop();
            }
        }
    }
}