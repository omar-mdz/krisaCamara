using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krisa.Datos;
using System.Data.Entity;

namespace Krisa.ControlUsuarios
{  
    public partial class UIModificarUsuario : Form
    {
       GestorUsuario gestorUsuario;

       public UIModificarUsuario()
        {
            InitializeComponent();
            gestorUsuario = new GestorUsuario();
        }

        //Accion del boton Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Metodo para modificar la contraseña del usuario
        public void ModificarUsuario()
        {
            Usuario usuario = new Usuario();
            usuario.Nombre_Usuario = txtUsuario.Text;
            usuario.Pass = txtPass.Text;

            string nuevoPass = txtNuevoPass.Text;

            gestorUsuario.ModificarUsuario(usuario, nuevoPass);
        }  

        //Metodo para validar campos de UI
        public void ValidarCampos() {
            if (txtUsuario.Text.Trim() != "" && txtPass.Text.Trim() != "" && txtConfirmar.Text.Trim() != "" && txtNuevoPass.Text.Trim() != "")
            {
                if (txtNuevoPass.Text == txtConfirmar.Text)
                {
                    ModificarUsuario();
                }
                else 
                {
                    MessageBox.Show("La nueva contraseña no coincide");
                }
            }
            else {
                MessageBox.Show("Todos los campos son obligatorios. Intente de nuevo");
            }
        }

        //Accion del boton Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ValidarCampos();
        }     
    }
} 
