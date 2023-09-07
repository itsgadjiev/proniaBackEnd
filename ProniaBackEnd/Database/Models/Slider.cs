using ProniaBackEnd.Database.Base;

namespace ProniaBackEnd.Database.Models
{

    public class Slider : BaseEntity, IAuditable
    {
        public static int _idCounter;
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
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }


    }
}
