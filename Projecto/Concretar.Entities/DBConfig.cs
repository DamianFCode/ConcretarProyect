using System.IO;
using Microsoft.Extensions.Configuration;

namespace Concretar.Entities
{
    public class DbConfig 
    {
        private static IConfigurationRoot conexion;

        /// <summary>
        /// Genera una instancia única para el manejo de contexto de app
        /// </summary>
        public static IConfigurationRoot Conexion
        {
            get
            {
                if (conexion == null)
                {
                    var configuraciones = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

                    string stagingEnvironment = configuraciones["StagingEnvironment"];

                    conexion = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{stagingEnvironment}.json", optional: true)
                        .Build();
                }
                return conexion;
            }
        }
    }
}
