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
        private static UILogin_Usuario UILogin_Usuario;
        private static CrearUsuario UIControl_Usuario;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CrearUsuario());

            UILogin_Usuario = new UILogin_Usuario();
            UILogin_Usuario.ShowDialog();

            if (UILogin_Usuario.pbolAccesoCorrecto.Equals(true))
            {
                UIControl_Usuario = new CrearUsuario();
                Application.Run(UIControl_Usuario);
            }
        }
    }
}
