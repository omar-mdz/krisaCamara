using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    /// <summary>
    /// Este clase representa un error de driver de la tarjeta
    /// </summary>
    /// <remarks>Para isolar otros proyectos de dependencias a NationalInstruments.DAQmx</remarks>
    public class DriverException : Exception
    {
        internal DriverException(DaqException ex) : base (ex.Message, ex)
        {
        }
    }
}
