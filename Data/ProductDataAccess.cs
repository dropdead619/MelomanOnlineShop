using Models;
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
            var selectSqlScript = $"select p.id, p.name, a.id as authorId, a.name as authorName, g.id as genreId, g.name as genreName, c.name as category, " +
                $"cm.comment as comments, price, quantity, rating from Products p " +
                $"join Authors a on a.id = p.authorId " +
                $"join Genres g on g.id = p.genreId " +
                $"join Categories c on c.id = p.categoryId " +
                $"join Commentaries cm on cm.id = p.commentaryId";
            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var products = new List<Product>();

            while (dataReader.Read())
            {
                products.Add(new Product
                {
                    Id = Guid.Parse(dataReader["id"].ToString()),
                    Name = dataReader["name"].ToString(),
                    AuthorName = new Author
                    {
                        Id = Guid.Parse(dataReader["authorId"].ToString()),
                        Name = dataReader["authorName"].ToString()
                    },
                    GenreName = new Genre
                    {
                        Id = Guid.Parse(dataReader["genreId"].ToString()),
                        Name = dataReader["genreName"].ToString()
                    },
                    Comments = new List<string> {
                       dataReader["comments"].ToString() },
                    Price = double.Parse(dataReader["price"].ToString()),
                    Quantity = int.Parse(dataReader["quantity"].ToString()),
                    Category = dataReader["category"].ToString(),
                    Rating = double.Parse(dataReader["rating"].ToString())
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
