

using Krisa.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace Krisa.ControlUsuarios
{
    public partial class UICrearUsuario : Form
    {
        GestorUsuario gestorUsuario;

        public UICrearUsuario()
        {
            InitializeComponent();
            gestorUsuario = new GestorUsuario();
        }

        //Metodo para Agregar un usuario
        public void AgregarUsuario()
        {
            //Crear un objeto usuario
            Usuario usuario = new Usuario();
            usuario.Nombre_Usuario = txtUsuario.Text;
            usuario.Nombre_Completo = txtNombreCompleto.Text;
            usuario.Pass = txtPass.Text;
            usuario.isActivo = true; 

            //Llamar gestor de usuarios para agregar el usuario
            gestorUsuario.AgregarUsuario(usuario);
        }

        //Acccion del boton Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Accion del boton Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()) {
                AgregarUsuario();
                Limpiar();
            }   
        }

        //Metodo para limpiar los Cuadros de texto
        public void Limpiar() {
            txtUsuario.Text = "";
            txtNombreCompleto.Text = "";
            txtPass.Text = "";
            txtPassConfirmacion.Text = "";
        }

        //Metodo para validar los cuadros de texto
        public bool ValidarCampos() {
            bool ban = true;

            if (txtUsuario.Text.Trim() == "" || txtNombreCompleto.Text.Trim() == "" || txtPass.Text == "" || txtPassConfirmacion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios. Intente de Nuevo");
                ban = false;
            }
            else
            {
                if (txtPass.Text != txtPassConfirmacion.Text)
                {
                    MessageBox.Show("La contraseña no coincide. Intente de Nuevo");
                    ban = false;
                    txtPass.Text = "";
                    txtPassConfirmacion.Text = "";
                }
            }
            return ban;
        }

        //
       private void txtPassConfirmacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (ValidarCampos())
                {
                    AgregarUsuario();
                    Limpiar();
                }  
            }
        }

        private void txtNombreCompleto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para validad que solo se introduzcan letras
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
