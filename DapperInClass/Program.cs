using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace DapperInClass
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection connection = new MySqlConnection(connString);

            var repo = new ProductRepository(connection);

            repo.CreateProduct("newStuff", 20, 1);
            repo.UpdateProduct(940, "notNewStuff");
            repo.DeleteProduct(941); //interesting how it keeps shifting CreateProduct "newStuff" to a new ProductID ... now at 942
            
            var products = repo.GetAllProducts();

            foreach (var pro in products)
            {
                Console.WriteLine($"{pro.ProductID} {pro.Name} {pro.Price} {pro.CategoryID} {pro.OnSale} {pro.StockLevel}");
            }
        }
    }
}