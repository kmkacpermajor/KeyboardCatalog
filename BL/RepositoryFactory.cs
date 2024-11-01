using INTERFACES;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class RepositoryFactory
    {
        private static IConfigurationRoot _config;

        static RepositoryFactory()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        public static IProductRepository CreateProductRepository()
        {
            // Pobranie nazwy typu z pliku konfiguracyjnego
            string typeName = _config["DAOImplementation"];
            if (string.IsNullOrEmpty(typeName))
                throw new InvalidOperationException("Nie znaleziono typu DAO w konfiguracji.");

            // Załaduj odpowiednie assembly i utwórz instancję
            var type = Type.GetType(typeName);
            if (type == null)
                throw new TypeLoadException($"Nie udało się załadować typu: {typeName}");

            return (IProductRepository)Activator.CreateInstance(type);
        }

        public static IManufacturerRepository CreateManufacturerRepository()
        {
            // Pobranie nazwy typu z pliku konfiguracyjnego
            string typeName = _config["DAOImplementation"];
            if (string.IsNullOrEmpty(typeName))
                throw new InvalidOperationException("Nie znaleziono typu DAO w konfiguracji.");

            // Załaduj odpowiednie assembly i utwórz instancję
            var type = Type.GetType(typeName);
            if (type == null)
                throw new TypeLoadException($"Nie udało się załadować typu: {typeName}");

            return (IManufacturerRepository)Activator.CreateInstance(type);
        }
    }
}
