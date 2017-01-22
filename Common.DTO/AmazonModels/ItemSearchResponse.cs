using System.Xml.Serialization;

namespace Common.DTO.AmazonModels
{
    public class ItemSearchResponse
    {
        [XmlElement("Items")]
        public Items[] Items { get; set; }
    }
}
