﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Author
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
