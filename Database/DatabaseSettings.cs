namespace CPSDevExerciseWeb.Database
{
    /// <summary>
    /// A helper class to obtain the database's connection string
    /// </summary>
    public static class DatabaseSettings
    {
        static IConfiguration? _configuration;
        public static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                if (_configuration == null)
                {
                    _configuration = value;
                }
            }
        }


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                var dbSettings = Configuration.GetSection("DatabaseSettings");

                var host = dbSettings["DB_HOSTNAME"];
                var port = dbSettings["DB_PORT"];
                var database = dbSettings["DB_DB_NAME"];
                var username = dbSettings["DB_USERNAME"];
                var password = dbSettings["DB_PASSWORD"];
                var max_pool_size = dbSettings["DB_MAX_POOL_SIZE"];

                return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};MaxPoolSize={5};", host, port, database, username, password, max_pool_size);
            }
        }
    }
}
