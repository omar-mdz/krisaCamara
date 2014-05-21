using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krisa.ControlUsuarios
{
    public partial class UILogin_Usuario : Form
    {
        public UILogin_Usuario()
        {
            InitializeComponent();
        }

        public bool pbolAccesoCorrecto;

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidarCampos())
                {

                    pbolAccesoCorrecto = true;

                    if (this.pbolAccesoCorrecto.Equals(true))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El usuario no es válido.");
                    }
                }
            }
            catch (Exception lexcError)
            {
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CerrarFormulario();
        }

        private void CerrarFormulario()
        {
            this.Close();
        }

        private bool ValidarCampos()
        {
            epvError.Clear();
            bool lbolValidar = true;

            if (txtUsuario.Text == "")
            {
                lbolValidar = false;
                epvError.SetError(txtUsuario,"Especifique su usuario");
            }
            if (txtPassword.Text == "")
            {
                lbolValidar = false;
                epvError.SetError(txtPassword, "Especifique su password");
            }

            return lbolValidar;
        }
    }
}
