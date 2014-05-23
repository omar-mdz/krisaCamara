using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krisa.Datos;

namespace Krisa.Datos
{
    /// <summary>
    /// Esta clase representa el contexto de base de datos
    /// </summary>
    public class KrisaDB : DbContext
    {
        // Agregar clases para persistencia aqui
        // Ejemplo:
        // public DbSet<Estudiante> Estudiantes { get; set; }

      public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Elimina y crea de nuevo base de datos si el modelo ha cambiado.
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        private void RecrearBaseDeDatos()
        {
            Database.SetInitializer<KrisaDB>(new DropCreateDatabaseIfModelChanges<KrisaDB>());
        }
    }
}
