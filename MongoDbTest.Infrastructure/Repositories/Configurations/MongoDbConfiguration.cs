namespace MongoDbTest.Infrastructure.Repositories.Configurations
{
    /// <summary>
    /// MongoDB Configuration
    /// </summary>
    public class MongoDbConfiguration
    {
        /// <summary>
        /// Configuration Key
        /// </summary>
        /// <summary xml:lang="es">
        /// Clave de la configuración
        /// </summary>
        public const string Key = "MongoDbConfiguration";

        /// <summary>
        /// Configuration collection name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la colección de configuración
        /// </summary>
        public string ConfigurationCollectionName { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        /// <summary xml:lang="es">
        /// Cadena de conexión
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la bade de datos
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Simulation Collection Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la colección operaciones
        /// </summary>
        public string AccountsCollectionName { get; set; }
    }
}

