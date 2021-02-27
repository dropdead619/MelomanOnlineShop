using System;
using System.Collections.Generic;

namespace Models.Abstract
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author AuthorName { get; set; }
        public Genre GenreName { get; set; }
        public double Price { get; set; }
        public ICollection<string> Comments { get; set; }
        public double Rating { get; set; }

        // Конструктор не обязательно же создавать? Мы через свойства из БД вроде будем данные брать...

        //public Product(int id, string name, Author author, Genre genre, double price)
        //{
        //    if (id <= 0 && price <= 0)
        //    {
        //        Id = id;
        //        Price = price;
        //    }
        //    else
        //    {
        //        throw new Exception("Id and Price can't be less than zero!");
        //    }
        //    Name = name;
        //    AuthorName = author;
        //    GenreName = genre;
        //}
    }
}
