using Krisa.Datos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Krisa.Camara
{
    public class GestorCamara
    {
        public GestorCamara()
        {

        }

        /// <summary>
        /// Agregar una camara a la base de datos local
        /// </summary>
        /// <param name="nombre">Nombre que se le asignará a la cámara ejem (Camara superior)</param>
        /// <param name="dispositivo">Nombre del dispositivo que esta asociado a esta cámara</param>
        /// <returns>true = Si la cámara se agregó con éxito, false = Si ocurrio un error</returns>
        /// <exception cref="ApplicationException">La cámara ya se encuentra en la base de datos</exception>
        /// <exception cref="Exception">Error en Entity Framework</exception>
        public bool Agregar(string nombre, string dispositivo)
        {
            if (!Validar(nombre))
            {
                VideoCamara camara = new VideoCamara();
                camara.Nombre = nombre;
                camara.Dispositivo = dispositivo;
                camara.esActivo = true;
                try
                {
                    using (var db = new KrisaDBCliente())
                    {
                        db.camaras.Add(camara);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_CAMARA_EXISTE);
            }



        }

        /// <summary>
        /// Buscar una camara por su nombre en la base de datos
        /// </summary>
        /// <param name="nombreCamara">Nombre de la camara a buscar</param>
        /// <returns>Una camara con el nombre especificado</returns>
        public VideoCamara Buscar(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var resultado = from cam in db.camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    return resultado.First();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool Validar(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var resultado = from cam in db.camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    return resultado.Any();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Suspender una camara en la base de datos
        /// </summary>
        /// <param name="nombreCamara">Nombre de la camara a suspender</param>
        /// <returns>true = Si la cámara se suspendió con éxito, false = Si ocurrió un error</returns>
        public bool Suspender(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var suspender = from cam in db.camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    suspender.Single().esActivo = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Listar todas las cámaras de video que se encuentran conectadas al sistema.
        /// </summary>
        /// <returns>Cámaras conectadas y reconocidas por el sistema</returns>
        public Object[] ListarCamaras()
        {
            // TODO
            return null;
        }

        /// <summary>
        /// Obtener las cámaras que están registradas en la base de datos
        /// </summary>
        /// <returns>Cámaras registradas en la base de datos</returns>
        public List<VideoCamara> ObtenerCamaras()
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var suspender = from cam in db.camaras
                                    select cam;
                    return suspender.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
