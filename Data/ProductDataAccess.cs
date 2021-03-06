using Models;
using Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class ProductDataAccess : DbDataAccess<Product>
    {
        public override void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Product entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<Product> Select()
        {
            var selectSqlScript = $"Select * from Products";

            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var products = new List<Product>();

            while (dataReader.Read())
            {
                products.Add(new Book
                {
                   Id = Guid.Parse(dataReader["id"].ToString()),
                   Name = dataReader["name"].ToString(),
                   AuthorName = new Author { 
                       Id = Guid.Parse(dataReader["authorId"].ToString()),
                       Name = dataReader["authorName"].ToString() },
                   GenreName = new Genre { 
                       Id = Guid.Parse(dataReader["genreId"].ToString()),
                       Name = dataReader["genreName"].ToString()
                   },
                   Comments = dataReader["comments"].ToString(),
                   Price = int.Parse(dataReader["price"].ToString()),


                });
            }

            dataReader.Close();
            command.Dispose();
            return products;
        }

        public override void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
