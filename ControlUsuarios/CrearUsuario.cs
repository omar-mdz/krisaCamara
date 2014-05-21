

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
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        public void insertar()
        {
            if (ValidarCampos())
            {
                ControladorUsuarios verifica = new ControladorUsuarios();

                if (verifica.VerificarCuenta(txtUsuario.Text.Trim()))
                {
                    Guardar();
                    Limpiar();
                    MessageBox.Show("Usuario creado con exito.");
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya esta registrado.");
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            insertar();
        }

        public void Limpiar() {
            txtUsuario.Text = "";
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtPassConfirmacion.Text = "";
        }

        public bool ValidarCampos() {
            bool ban = true;

            if (txtUsuario.Text.Trim() == "" || txtNombreCompleto.Text.Trim() == "" || txtTelefono.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPass.Text == "" || txtPassConfirmacion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios. Intente de Nuevo.");
                ban = false;
            }
            else
            {
                if (txtPass.Text != txtPassConfirmacion.Text)
                {
                    MessageBox.Show("La contraseña no coincide. Intente de Nuevo.");
                    ban = false;
                }
            }
            return ban;
        }

      
        public void Guardar() 
        {
            try
            {
                Encriptacion encripta = new Encriptacion();
                string hash = encripta.Encriptar(txtPass.Text);

                var u = new KrisaDB.Usuario()
                {
                    Nombre_Usuario = txtUsuario.Text,
                    Nombre_Completo = txtNombreCompleto.Text,
                    Pass = hash,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    isActivo = true
                };

                var context = new KrisaDB();
                context.Usuarios.Add(u);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      
        private void btnCancelar_Click(object sender, EventArgs e)
        {
          Limpiar();
          this.Close();
        }

        private void txtPassConfirmacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                insertar();
            }
        }

        private void btnHistorialUsuario_Click(object sender, EventArgs e)
        {

        }

    }
}
