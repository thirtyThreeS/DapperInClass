using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace DapperInClass
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (name, price, categoryid) VALUES (@name, @price, @categoryid)",
                new {name = name, price = price, categoryID = categoryID});
        }

        //bonus - update
        public void UpdateProduct(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET name = @updatedName WHERE ProductID = @productID;",
                new {productID = productID, updatedName = updatedName});
        }

        //bonus - delete
        public void DeleteProduct(int productID) //have to destory EVERYTHING to remove from existence*
        {
            _connection.Execute("DELETE FROM products WHERE productID = @productID;",
                new { productID = productID });
            
            _connection.Execute("DELETE FROM sales WHERE productID = @productID;",
                new { productID = productID });
            
            _connection.Execute("DELETE FROM reviews WHERE productID = @productID;",
                new { productID = productID });
        }
        
        //public void DeleteNegativeRatingLol(int rating) --You are using SAFE MODE error.
        //{
        //    _connection.Execute("DELETE FROM reviews WHERE rating = @rating;",
        //        new {rating = rating});
        //}

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }
    }
}
