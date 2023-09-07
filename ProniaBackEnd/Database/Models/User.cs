﻿using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class User: BaseEntity , IAuditable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Basket Basket { get; set; }
    }
}