using System;
using System.Windows.Forms;
using StructureMap;

namespace TowerDefenseColab
{
    static class Program
    {
        private static Container GetIoC()
        {
            return new Container(_ => _.Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            }));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var ioc = GetIoC();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (GameWindow gw = ioc.GetInstance<GameWindow>())
            {
                gw.GameLoop();
            }
        }
    }
}