using Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Book:Product
    {
        public string Category { get; private set; } = "Книги";
    }
}
