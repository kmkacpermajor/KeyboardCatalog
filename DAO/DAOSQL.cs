using CORE;
using DAO.DAO;
using INTERFACES;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAOSQL : IProductRepository, IManufacturerRepository
    {
        private readonly string _connectionString = "Data Source=database.db;Version=3;";

        public DAOSQL()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string createProductsTable = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    ManufacturerId INTEGER,
                    FOREIGN KEY(ManufacturerId) REFERENCES Manufacturers(Id)
                );";

            string createManufacturersTable = @"
                CREATE TABLE IF NOT EXISTS Manufacturers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );";

            using var cmd = new SqliteCommand(createManufacturersTable, connection);
            cmd.ExecuteNonQuery();

            cmd.CommandText = createProductsTable;
            cmd.ExecuteNonQuery();
        }

        // Product methods
        public IEnumerable<IProduct> GetAllProducts()
        {
            var products = new List<IProduct>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("SELECT * FROM Products", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Type = (KeyboardType)Enum.Parse(typeof(KeyboardType), reader["Type"].ToString()),
                    Manufacturer = GetManufacturerById(Convert.ToInt32(reader["ManufacturerId"]))
                });
            }

            return products;
        }

        public IProduct GetProductById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = new SqliteCommand("SELECT * FROM Products WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Type = (KeyboardType)Enum.Parse(typeof(KeyboardType), reader["Type"].ToString()),
                    Manufacturer = GetManufacturerById(Convert.ToInt32(reader["ManufacturerId"]))
                };
                return product;
            }
            return null;
        }

        public void Add(IProduct product)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("INSERT INTO Products (Name, Type, ManufacturerId) VALUES (@Name, @Type, @ManufacturerId)", connection);

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Type", product.Type);
            command.Parameters.AddWithValue("@ManufacturerId", product.Manufacturer.Id);

            command.ExecuteNonQuery();
        }

        public void Update(IProduct product)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("UPDATE Products SET Name = @Name, Type = @Type, ManufacturerId = @ManufacturerId WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Type", product.Type);
            command.Parameters.AddWithValue("@ManufacturerId", product.Manufacturer.Id);

            command.ExecuteNonQuery();
        }

        public void DeleteProduct(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("DELETE FROM Products WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        // Manufacturer methods
        public IEnumerable<IManufacturer> GetAllManufacturers()
        {
            var manufacturers = new List<IManufacturer>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("SELECT * FROM Manufacturers", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                manufacturers.Add(new Manufacturer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                });
            }

            return manufacturers;
        }

        public IManufacturer GetManufacturerById(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("SELECT * FROM Manufacturers WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Manufacturer
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                };
            }

            return null;
        }

        public void Add(IManufacturer manufacturer)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("INSERT INTO Manufacturers (Name) VALUES (@Name)", connection);

            command.Parameters.AddWithValue("@Name", manufacturer.Name);

            command.ExecuteNonQuery();
        }

        public void Update(IManufacturer manufacturer)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("UPDATE Manufacturers SET Name = @Name WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", manufacturer.Id);
            command.Parameters.AddWithValue("@Name", manufacturer.Name);

            command.ExecuteNonQuery();
        }

        public void DeleteManufacturer(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = new SqliteCommand("DELETE FROM Manufacturers WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}
