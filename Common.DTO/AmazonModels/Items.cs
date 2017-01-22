using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.DTO.AmazonModels
{
    public class Items
    {
        [XmlElement("Item")]
        public Item[] Item { get; set; }
    }
}
