using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{
    public class Slider:BaseEntity
    {
        public static int _idCounter;

        public Slider(string title, string description, string image, string buttonUrl, byte order)
        {
            Id = ++_idCounter;
            Title = title;
            Description = description;
            Image = image;
            ButtonUrl = buttonUrl;
            Order = order;
        }
        public Slider()
        {

        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ButtonUrl { get; set; }
        public byte Order { get; set; }
        public string OfferText { get; set; }
        public bool Offering { get; set; }  

    }
}
