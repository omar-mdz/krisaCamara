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
    }
}
