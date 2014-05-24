using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    public static class Driver
    {
        /// <summary>
        /// Regresa los nombres de todas las tarjetas instaladas en el sistema.
        /// </summary>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public static string[] Tarjetas
        {
            get
            {
                try
                {
                    return DaqSystem.Local.Devices;
                }
                catch (DaqException ex)
                {
                    throw new DriverException(ex);
                }
            }
        }

        /// <summary>
        /// Abre la tarjeta con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre de la tarjeta para abrir</param>
        /// <returns>Informacion de la tarjeta</returns>
        /// <exception cref="ArgumentNullException">El nombre de la tarjeta es null</exception>
        /// <exception cref="ArgumentException">El nombre de la tarjeta no esta registrado en el sistema</exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        internal static Device AbrirTarjeta(string nombre)
        {
            try
            {
                // Validar en nombre
                if (nombre == null)
                {
                    throw new ArgumentNullException("nombre");
                }
                if (!DaqSystem.Local.Devices.Contains(nombre))
                {
                    throw new ArgumentException("El nombre de la tarjeta no es valido", "nombre");
                }
                // Abrir los datos de la tarjeta
                return DaqSystem.Local.LoadDevice(nombre);
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
        }
    }
}
