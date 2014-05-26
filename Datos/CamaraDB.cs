using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Clase que realiza operaciones CRUD (Create, Read, Update, Delete) 
    /// sobre la clase VideoCamara
    /// </summary>
    public class CamaraDB : IDisposable
    {
        KrisaDBCliente camarasDb;
        public CamaraDB()
        {
            camarasDb = new KrisaDBCliente();
        }

        /// <summary>
        /// Método que agrega una VideoCamara a la base de datos
        /// </summary>
        /// <param name="camara">VideoCamara</param>
        public void Agregar(VideoCamara camara)
        {
            try
            {
                camarasDb.camaras.Add(camara);
                camarasDb.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                camarasDb.Dispose();
            }
        }


        /// <summary>
        /// Método que suspende una Videocamara en la base de datos local
        /// </summary>
        /// <param name="camara">VideoCamara que existe en la base de datos local</param>
        public void Suspender(VideoCamara camara)
        {
            try
            {
                VideoCamara camaraSuspendida = new VideoCamara();
                camaraSuspendida = Buscar(camara.Nombre);
                camaraSuspendida.esActivo = false;
                camarasDb.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                camarasDb.Dispose();
            }

        }

        public VideoCamara Buscar(string nombreCamara)
        {
            try
            {
                var resultado = from cam in camarasDb.camaras
                                where cam.Nombre == nombreCamara

                                select cam;
                return resultado.First<VideoCamara>();
            }
            catch (Exception e)
            {
                camarasDb.Dispose();
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Método que encuentra todas las videocamaras que se encuentran en la base de datos
        /// </summary>
        /// <returns>Lista de VideoCamaras</returns>
        public List<VideoCamara> EncontrarTodas()
        {

            var resultado = from cam in camarasDb.camaras select cam;
            
            return resultado.ToList<VideoCamara>();
        }

        public void Dispose()
        {
            camarasDb.Dispose();
        }

    }
}
