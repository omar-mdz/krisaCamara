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

        public Tarjeta(string nombre) : this(Driver.AbrirTarjeta(nombre))
        {
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
        /// Lee el valor de un puerto de entrada binario
        /// </summary>
        /// <param name="nombre">Nombre de puerto de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de un puerto</returns>
        public bool[] LeerBinario(string nombre, bool persistente = false)
        {
            return LeerBinario(new string[] { nombre }, persistente);
        }

        /// <summary>
        /// Lee los valores de unos puertos de entrada binarios
        /// </summary>
        /// <param name="nombres">Nombres de puertos de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de los puertos</returns>
        public bool[] LeerBinario(string[] nombres, bool persistente = false)
        {
            Task digitalReadTask = null;
            try
            {
                // Generar nombre de la tarea
                var nombreTarea = "L" + String.Join(",", nombres);
                // Si la tarea esta creada antes
                if (tareas.ContainsKey(nombreTarea))
                {
                    digitalReadTask = tareas[nombreTarea];
                    persistente = true;
                }
                else
                {
                    // Crear nueva tarea
                    digitalReadTask = new Task();
                    // Agregar los canales
                    digitalReadTask.DIChannels.CreateChannel(
                        String.Join(",", nombres),
                        "",
                        ChannelLineGrouping.OneChannelForAllLines);
                    // Guardar tarea persistente
                    if (persistente)
                    {
                        tareas.Add(nombreTarea, digitalReadTask);
                    }
                }
                // Leer los datos
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
        /// Escribe el valor de un puerto de salida binario
        /// </summary>
        /// <param name="nombre">Nombre de puerto de salida</param>
        /// <param name="data">Valor para escribir</param>
        /// <param name="persistente">true - para crear una tarea de escritura permanente</param>
        public void EscribirBinario(string nombre, bool[] data, bool persistente = false)
        {
            EscribirBinario(new string[] { nombre }, data, persistente);
        }

        /// <summary>
        /// Escribe los valores de unos puertos de salida binarios
        /// </summary>
        /// <param name="nombres">Nombres de puertos de salida</param>
        /// <param name="data">Valor para escribir</param>
        /// <param name="persistente">true - para crear una tarea de escritura permanente</param>
        public void EscribirBinario(string[] nombres, bool[] data, bool persistente = false)
        {
            Task digitalWriteTask = null;
            try
            {
                // Generar nombre de la tarea
                var nombreTarea = "E" + String.Join(",", nombres);
                // Si la tarea esta creada antes
                if (tareas.ContainsKey(nombreTarea))
                {
                    digitalWriteTask = tareas[nombreTarea];
                    persistente = true;
                }
                else
                {
                    // Crear nueva tarea
                    digitalWriteTask = new Task();
                    // Agregar los canales
                    digitalWriteTask.DOChannels.CreateChannel(
                        String.Join(",", nombres),
                        "",
                        ChannelLineGrouping.OneChannelForAllLines);
                    // Guardar tarea persistente
                    if (persistente)
                    {
                        tareas.Add(nombreTarea, digitalWriteTask);
                    }
                }
                // Escribir los datos
                var writer = new DigitalSingleChannelWriter(digitalWriteTask.Stream);
                writer.WriteSingleSampleMultiLine(true, data);
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
            finally
            {
                if (!persistente && (digitalWriteTask != null))
                {
                    digitalWriteTask.Dispose();
                    digitalWriteTask = null;
                }
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
