using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common.DTO.AmazonModels
{
    public class Offers
    {
        [XmlElement("Offer")]
        public Offer[] Offer { get; set; }
    }
}
