using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krisa.Datos;
using System.Security.Cryptography;

namespace Krisa.ControlUsuarios
{
    public class GestorUsuario
    {
        UsuarioBD userDB;

        public GestorUsuario()
        {
            userDB = new UsuarioBD();
        }

        //Agregar un usuario a la base de datos
        public void AgregarUsuario(Usuario usuario)
        {
            string hash = Encriptar(usuario.Pass);
            usuario.Pass = hash;

            userDB.AgregarUsuario(usuario);
        }

        //Modificar la contraseña de un usuario
        public void ModificarUsuario(Usuario usuario, string nuevoPass)
        {
            string nuevohash = Encriptar(nuevoPass);
            string hash = Encriptar(usuario.Pass);
            usuario.Pass = hash;

            userDB.ModificarUsuario(usuario, nuevohash);
        }

        //Encriptar la contraseña del usuario
        public string Encriptar(string password)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
            {
                output.Append(hashedBytes[i].ToString("x2").ToLower());
            }
            return output.ToString();
        }
    }
}
