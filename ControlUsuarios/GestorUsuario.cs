using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Krisa.Datos;

namespace Krisa.ControlUsuarios
{
    public class GestorUsuario
    {
        public KrisaDB db;

        public GestorUsuario()
        {
            db = new KrisaDB();
        }

        /// <summary>
        /// Metodo para agregar un usuario a la Base de Datos
        /// </summary>
        /// <param name="usuario"></param>
        public bool AgregarUsuario(Usuario usuario)
        {
            if (VerificarUsuario(usuario))
            {
                string contrasenaEncriptada = Encriptar(usuario.Contrasena);
                usuario.Contrasena = contrasenaEncriptada;
                try
                {
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Error de conexion en la Base de Datos");
                }
            }
            else
            {
                throw new Exception("El usuario ya esta registrado");
            }
        }

        /// <summary>
        /// Metodo para modificar la contraseña de un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="nuevoPass"></param>
        public bool ModificarUsuario(Usuario usuario, string nuevaContrasena)
        {   
            string ContrasenaEncriptada = Encriptar(usuario.Contrasena);
            usuario.Contrasena = ContrasenaEncriptada;
            if (ValidarContrasena(usuario.Contrasena))
            {
                try
                {
                    string nuevaContrasenaEncriptada = Encriptar(nuevaContrasena);
                    var cambio = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                    cambio.First().Contrasena = nuevaContrasenaEncriptada;
                    db.SaveChanges();
                    db.Dispose();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Error de conexion en la Base de Datos");   
                }
            }
            else
            {
                throw new Exception("La contraseña es incorrecta");
            }
        }

        /// <summary>
        /// Metodo para encriptar la contraseña del usuario
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Encriptar(string contrasena)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
            {
                output.Append(hashedBytes[i].ToString("x2").ToLower());
            }
            return output.ToString();
        }

        /// <summary>
        /// Metodo para validar la contraseña anterior de un usuario
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidarContrasena(string contrasena)
        {
            try
            {
                var resultado = from user in db.Usuarios where user.Contrasena == contrasena select user;
                return resultado.Count<Usuario>() == 0 ? false : true;
            }
            catch (Exception)
            {
                throw new Exception("Error de conexion en la Base de Datos");
            }
        }

        /// <summary>
        /// Metodo para verificar si un usuario ya esta registrado en la BD
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool VerificarUsuario(Usuario usuario)
        {
            try
            {
                var resultado = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                return resultado.Count<Usuario>() == 0 ? true : false;
            }
            catch (Exception)
            {
                throw new Exception("Error de conexion en la Base de Datos");
            }
        }
    }
}

