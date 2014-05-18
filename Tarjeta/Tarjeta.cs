using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    public class Tarjeta : IDisposable
    {
        /// <summary>
        /// Handle de tarjeta
        /// </summary>
        private Device device;
        /// <summary>
        /// La lista de las tareas para los puertos y canales
        /// </summary>
        private Dictionary<string, Task> tareas;

        internal Tarjeta(Device device)
        {
            this.device = device;
            tareas = new Dictionary<string, Task>();
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

        /// <summary>
        /// Creas una parea de lectura de los datos digitales
        /// </summary>
        /// <param name="nombre">Nombres de los puertos</param>
        /// <param name="persistente">true - para hacer tarea persistente</param>
        /// <returns>Tarea creada</returns>
        private Task CrearTareaLectura(string[] nombres, bool persistente)
        {
            // Si la tarea esta creada antes
            var nombreTarea = String.Join(";", nombres);
            if (tareas.ContainsKey(nombreTarea))
            {
                return tareas[nombreTarea];
            }
            // Crear nueva tarea
            var tarea = new Task();
            // Agregar los canales
            foreach (var nombre in nombres)
            {
                tarea.DIChannels.CreateChannel(
                    nombre,
                    nombre.Split(new char[] { '/' }, 2)[1].Replace('/', '-'),
                    ChannelLineGrouping.OneChannelForAllLines);
            }

            if (persistente)
            {
                tareas.Add(nombreTarea, tarea);
            }

            return tarea;
        }

        /// <summary>
        /// Lee el valor de un puerto de entrada
        /// </summary>
        /// <param name="nombre">Nombre de puerto de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de un puerto</returns>
        public bool[] Leer(string nombre, bool persistente = false)
        {
            return Leer(new string[] { nombre }, persistente);
        }

        /// <summary>
        /// Lee los valores de unos puertos de entrada
        /// </summary>
        /// <param name="nombres">Nombres de puertos de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de los puertos</returns>
        public bool[] Leer(string[] nombres, bool persistente = false)
        {
            Task digitalReadTask = null;
            try
            {
                digitalReadTask = CrearTareaLectura(nombres, persistente);
                var reader = new DigitalSingleChannelReader(digitalReadTask.Stream);
                return reader.ReadSingleSampleMultiLine();
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
            finally
            {
                if (!persistente && (digitalReadTask != null))
                {
                    digitalReadTask.Dispose();
                    digitalReadTask = null;
                }
            }
        }

        /// <summary>
        /// Escribe el valor de un puerto de salida
        /// </summary>
        /// <param name="nombre">Nombre de puerto de salida</param>
        /// <param name="data">Valor para escribir</param>
        public void Escribir(string nombre, bool[] data)
        {
            Escribir(new string[] { nombre }, data);
        }

        /// <summary>
        /// Escribe los valores de unos puertos de salida
        /// </summary>
        /// <param name="nombres">Nombres de puertos de salida</param>
        /// <param name="data">Valor para escribir</param>
        public void Escribir(string[] nombres, bool[] data)
        {
            try
            {
                using (Task digitalWriteTask = new Task())
                {
                    digitalWriteTask.DOChannels.CreateChannel(
                        String.Join(",", nombres),
                        "",
                        ChannelLineGrouping.OneChannelForAllLines);
                    var writer = new DigitalSingleChannelWriter(digitalWriteTask.Stream);
                    writer.WriteSingleSampleMultiLine(true, data);
                }
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            // Cerrar todas tareas
            foreach (var tarea in tareas)
            {
                tarea.Value.Dispose();
            }
            tareas.Clear();
            // Cerrar tarjeta
            device.Dispose();
            device = null;
        }
    }
}
