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
        /// <param name="nombre">Nombre de usuario para agregar a la base de datos</param>
        /// <param name="nombreCompleto">Nombre completo de usuario</param>
        /// <param name="contrasena">Contrasena de usuario</param>
        /// <returns>true = si el usuario fue agregado con exito, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        /// <exception cref="ApplicationException">El usuario con nombre especificado ya esta registrado</excepcion>
        public bool AgregarUsuario(string nombre, string nombreCompleto, string contrasena)
        {
            if (VerificarUsuario(nombre))
            {
                string contrasenaEncriptada = Encriptar(contrasena);
                var usuario = new Usuario()
                {
                    Nombre = nombre,
                    NombreCompleto = nombreCompleto,
                    Contrasena = contrasenaEncriptada,
                    Activo = true
                };
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
                    throw;
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_AGREGAR_USUARIO);
            }
        }

        /// <summary>
        /// Modificar la contraseña de un usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario para cambiar la contraseña</param>
        /// <param name="contrasena">Contraseña actual del usuario</param>
        /// <param name="nuevaContrasena">Nueva contraseña del usuario</param>
        /// <returns>true = se modifico el usuario, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        /// <exception cref="ApplicationException">La constraseña especificada de usuario no es correcta</excepcion>
        public bool ModificarUsuario(string nombre, string contrasena, string nuevaContrasena)
        {
            string contrasenaEncriptada = Encriptar(contrasena);
            if (ValidarContrasena(nombre, contrasenaEncriptada))
            {
                try
                {
                    using (var db = new KrisaDB())
                    {
                        string nuevaContrasenaEncriptada = Encriptar(nuevaContrasena);
                        var cambio = from user in db.Usuarios
                                     where user.Nombre == nombre
                                     select user;
                        cambio.Single().Contrasena = nuevaContrasenaEncriptada;
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw; 
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_CONTRASENA);
            }
        }

        /// <summary>
        /// Encriptar la contraseña del usuario
        /// </summary>
        /// <param name="contrasena">Contraseña que se va a encriptar con el SHA256</param>
        /// <returns>Cadena de la contraseña encriptada en algoritmo SHA256</returns>
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
        /// Validar la contraseña anterior de un usuario
        /// </summary>
        /// <param name="nombre">Nombre del usuario para validar su contraseña</param>
        /// <param name="hash">Contraseñ encriptada del usuario para validar</param>
        /// <returns>true = contraseña correcta, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        public bool ValidarContrasena(string nombre, string hash)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios
                                    where user.Nombre == nombre && user.Contrasena == hash
                                    select user;
                    return resultado.Any() ? false : true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verificar si un usuario ya esta registrado en la Base de datos
        /// </summary>
        /// <param name="nombre">Nombre de usuario para verificar si existe en la base de datos</param>
        /// <returns>true = el usuario no esta registrado en la base de datos, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        public bool VerificarUsuario(string nombre)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios
                                    where user.Nombre == nombre
                                    select user;
                    return resultado.Any();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

