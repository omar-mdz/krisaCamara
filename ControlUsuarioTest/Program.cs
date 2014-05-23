using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krisa.ControlUsuarios;

namespace Krisa.ControlUsuarios.Test
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        private static UICrearUsuario UILogin_Usuario;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CrearUsuario());

            UILogin_Usuario = new UICrearUsuario();
            UILogin_Usuario.ShowDialog();
   
        }
    }
}
