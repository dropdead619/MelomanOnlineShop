using System;
using System.Collections.Generic;

namespace Models.Abstract
{
    public abstract class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Author AuthorName { get; set; }
        public Genre GenreName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public ICollection<string> Comments { get; set; }
        public ICollection<double> Rating { get; set; }
    }
}
