using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Movie:Product
    {
        public string Category { get; private set; } = "Фильмы";
    }
}
