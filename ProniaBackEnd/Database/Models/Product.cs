using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class Product : BaseEntity
    {
        public static int _idCounter = 1;
        public string ProductName{ get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public byte Order { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsModified { get; set; }

        public Product(string productName,string description,string image,string color , string size , double price ,byte order)
        {
            Id = _idCounter++;
            ProductName = productName;
            Description = description;
            Image = image;
            Color = color;
            Size = size;
            Price = price;
            Order = order;
            CreationDate = DateTime.Now;
            IsModified = false;
            LastModifiedDate = default;
        }
    }
}
