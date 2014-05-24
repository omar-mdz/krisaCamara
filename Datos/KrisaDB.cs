using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    public class KrisaDB : DbContext
    {
        // Agregar clases para persistencia en el servidor aqui
        // Ejemplo:
        // public DbSet<Estudiante> Estudiantes { get; set; }

        /// <summary>
        /// Constructor de clase KrisaDB
        /// </summary>
        public KrisaDB() : base()
        {
            ConfigurarConexion();
        }

        /// <summary>
        /// Configura la conexion con la base de datos
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        private void ConfigurarConexion()
        {
            Database.Connection.ConnectionString = "Data Source=(LocalDB)\\v11.0;Integrated Security=True;Database=KrisaDB";
            Database.SetInitializer<KrisaDB>(new DropCreateDatabaseIfModelChanges<KrisaDB>());
        }
    }
}
