using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krisa.Datos;

namespace Krisa.Camara
{
    public class GestorCamara
    {
        public GestorCamara()
        {

        }

        /// <summary>
        /// Método que permite agregar una camara mediante el gestor de cámaras
        /// </summary>
        /// <param name="nombre">Nombre que se le asignará a la cámara ejem (Camara superior)</param>
        /// <param name="dispositivo">Nombre del dispositivo que esta asociado a esta cámara</param>
        public void Agregar(string nombre, string dispositivo)
        {
            VideoCamara camara = new VideoCamara();
            camara.Nombre = nombre;
            camara.Dispositivo = dispositivo;
            camara.esActivo = true;

            try
            {
                using (var db = new CamaraDB())
                {
                    db.Agregar(camara);
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Método que permite buscar una camara por su nombre
        /// </summary>
        /// <param name="nombreCamara">Nombre de la camara a buscar</param>
        /// <returns>Una camara</returns>
        public VideoCamara Buscar(string nombreCamara)
        {
            try
            {
                using (var db = new CamaraDB())
                {
                    return db.Buscar(nombreCamara);
                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
    }
}
