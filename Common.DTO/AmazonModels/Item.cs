using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.DTO.AmazonModels
{
    public class Item
    {
        public string ASIN { get; set; }
        public string DetailPageURL { get; set; }
        public ItemAttributes ItemAttributes { get; set; }
        public OfferSummary OfferSummary { get; set; }
        public Offers Offers { get; set; }
    }
}
