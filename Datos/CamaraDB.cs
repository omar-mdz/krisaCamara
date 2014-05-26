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
    public class CamaraDB
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
            var resultado = from cam in camarasDb.camaras
                            where cam.Nombre == nombreCamara

                            select cam;
            return resultado.First<VideoCamara>();
        }
    }
}
