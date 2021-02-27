using Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Music:Product
    {
        public string Category { get; private set; } = "Музыка";
    }
}
