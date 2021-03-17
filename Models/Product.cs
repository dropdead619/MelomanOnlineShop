using Models.Abstract;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public Author AuthorName { get; set; }
        public Genre GenreName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<string> Comments { get; set; }
        public double Rating { get; set; }
        public string Category { get; set; }
    }
}
