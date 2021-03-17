using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Abstract
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
