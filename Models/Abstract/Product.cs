using System;

namespace Models.Abstract
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author AuthorName { get; set; }
        public Genre GenreName { get; set; }
        public double Price { get; set; }
        public Product(int id, string name, Author author, Genre genre, double price)
        {
            if(id <= 0 && price <=0)
            {
                Id = id;
                Price = price;
            } else
            {
                throw new Exception("Id and Price can't be less than zero!");
            }
            Name = name;
            AuthorName = author;
            GenreName = genre;
        }
    }
}
