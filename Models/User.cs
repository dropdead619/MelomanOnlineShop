using System;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PhoneNumber { get; set; }
        
    }
}
