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
        internal static Device AbrirTarjeta(string nombre)
        {
            try
            {
                return DaqSystem.Local.LoadDevice(nombre);
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
        }
    }
}
