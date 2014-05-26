using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Krisa.Datos;

namespace Krisa.Tarjeta
{
    public class GestorTarjeta
    {
        /// <summary>
        /// Agrega a tarjeta en la base de datos
        /// </summary>
        /// <param name="nombre">Nombre de la tarjeta</param>
        /// <param name="direccion">Direccion fisica de la tarjeta</param>
        public void AgregarTarjeta(string nombre, string direccion)
        {
            var tarjeta = new Datos.Tarjeta()
            {
                Nombre = nombre,
                Direccion = direccion
            };
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    db.Tarjetas.Add(tarjeta);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
