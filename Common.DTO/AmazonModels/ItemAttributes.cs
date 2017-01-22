using System.Xml.Serialization;

namespace Common.DTO.AmazonModels
{
    public class ItemAttributes
    {
        [XmlElement("Author")]
        public string Author { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public Price ListPrice { get; set; }
    }
}
