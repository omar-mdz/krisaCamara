using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Control_Usuarios.Delegados;

namespace Krisa.ControlUsuarios
{
    public delegate void VisibilidadFormulario(bool abolFormulario);
    public partial class UIModificar_Usuario : Form
    {
        public VisibilidadFormulario pDelVisibilidadFormulario;

        public UIModificar_Usuario()
        {
            InitializeComponent();
        }

        private void frmControlUsuarioModificar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pDelVisibilidadFormulario != null)
            {
                pDelVisibilidadFormulario(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CerrarVentana();
        }

        private void CerrarVentana()
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //aqui de necesita el codigo
        }
    }
}
