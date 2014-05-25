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
        /// <summary>
        /// Agregar un usuario a la Base de Datos
        /// </summary>
        /// <param name="usuario">Es el usuario que se va a agregar a la base de datos</param 
        public bool AgregarUsuario(Usuario usuario)
        {
            if (VerificarUsuario(usuario))
            {
                string contrasenaEncriptada = Encriptar(usuario.Contrasena);
                usuario.Contrasena = contrasenaEncriptada;
                try
                {
                    using (var db = new KrisaDB())
                    {
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw new Exception(Krisa.Recursos.ERROR_CONEXION);
                }
            }
            else
            {
                throw new Exception(Krisa.Recursos.ERROR_AGREGAR_USUARIO);
            }
        }

        /// <summary>
        /// Metodo para modificar la contraseña de un usuario
        /// </summary>
        /// <param name="usuario">Es el usuario que se va a modificar en la Base de datos</param>
        /// <param name="nuevoPass">Es la nueva contraseña del usuario</param>
        public bool ModificarUsuario(Usuario usuario, string nuevaContrasena)
        {   
            string ContrasenaEncriptada = Encriptar(usuario.Contrasena);
            usuario.Contrasena = ContrasenaEncriptada;
            if (ValidarContrasena(usuario))
            {
                try
                {
                    using (var db = new KrisaDB())
                    {
                        string nuevaContrasenaEncriptada = Encriptar(nuevaContrasena);
                        var cambio = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                        cambio.First().Contrasena = nuevaContrasenaEncriptada;
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw new Exception(Krisa.Recursos.ERROR_CONEXION);   
                }
            }
            else
            {
                throw new Exception(Krisa.Recursos.ERROR_CONTRASENA);
            }
        }

        /// <summary>
        /// Metodo para encriptar la contraseña del usuario
        /// </summary>
        /// <param name="password">Es la contraseña que se va a encriptar con el SHA256</param>
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
        /// <param name="contrasena">Es la contraseña que se va a validar en la base de datos</param>
        /// <returns></returns>
        public bool ValidarContrasena(Usuario usuario)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios where user.Nombre == usuario.Nombre && user.Contrasena == usuario.Contrasena select user;
                    return resultado.Count<Usuario>() == 0 ? false : true;
                }
            }
            catch (Exception)
            {
                throw new Exception(Krisa.Recursos.ERROR_CONEXION);
            }
        }

        /// <summary>
        /// Metodo para verificar si un usuario ya esta registrado en la BD
        /// </summary>
        /// <param name="usuario">Es el usuario que se va verficar si existe en la base de datos</param>
        /// <returns></returns>
        public bool VerificarUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                    return resultado.Count<Usuario>() == 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw new Exception(Krisa.Recursos.ERROR_CONEXION);
            }
        }
    }
}

