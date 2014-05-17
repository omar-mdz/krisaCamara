using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    public class Tarjeta
    {
        /// <summary>
        /// Handle de tarjeta
        /// </summary>
        private Device device;

        internal Tarjeta(Device device)
        {
            this.device = device;
        }

        /// <summary>
        /// Regresa si esta tarjeta es un emulador
        /// </summary>
        public bool Emulador
        {
            get
            {
                try
                {
                    return device.IsSimulated;
                }
                catch (DaqException ex)
                {
                    throw new DriverException(ex);
                }
            }
        }

        /// <summary>
        /// Regresa los nombres de los canales digitales de esta tarjeta
        /// </summary>
        public string[] CanalesDigitales
        {
            get
            {
                try
                {
                    return device.DILines;
                }
                catch (DaqException ex)
                {
                    throw new DriverException(ex);
                }
            }
        }

        /// <summary>
        /// Regresa los nombres de los puertos digitales de esta tarjeta
        /// </summary>
        public string[] PuertosDigitales
        {
            get
            {
                try
                {
                    return device.DIPorts;
                }
                catch (DaqException ex)
                {
                    throw new DriverException(ex);
                }
            }
        }
    }
}
