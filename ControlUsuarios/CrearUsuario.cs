

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
using System.Text.RegularExpressions;


namespace Krisa.ControlUsuarios
{
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }
        
        //Metodo para realizar la insercion del usuario
        public void Insertar()
        {
            if (ValidarCampos())
            {
                ControladorUsuarios verifica = new ControladorUsuarios();

                if (verifica.VerificarCuenta(txtUsuario.Text.Trim()))
                {
                    Guardar();
                    Limpiar();
                    MessageBox.Show("Usuario creado con exito");
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya esta registrado");
                    Limpiar();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar();
        }

        //Metodo para limpiar los Cuadros de texto
        public void Limpiar() {
            txtUsuario.Text = "";
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtPassConfirmacion.Text = "";
        }

        //Metodo para validar los cuadros de texto
        public bool ValidarCampos() {
            bool ban = true;

            if (txtUsuario.Text.Trim() == "" || txtNombreCompleto.Text.Trim() == "" || txtTelefono.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPass.Text == "" || txtPassConfirmacion.Text == "")
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
                else 
                {
                    if (!ValidarEmail(txtEmail.Text))
                    {
                        MessageBox.Show("El e-mail no esta escrito correctamente. Intente de Nuevo");
                        txtEmail.Text = "";
                        ban = false;
                    }
                }
            }
            return ban;
        }

        //Metodo para Guardar el usuario en la Base de Datos
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
          this.Close();
        }

        private void txtPassConfirmacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Insertar();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
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
        //Validacion de Email
        private bool ValidarEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
