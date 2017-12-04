using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Editor_Graficación_2D
{
    static class Program
    {
        /// <summary>
        ///Punto de partida de la aplicacion.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
    }
}