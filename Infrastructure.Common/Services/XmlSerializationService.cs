using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Infrastructure.Common.Services
{
    public class XmlSerializationService
    {
        public string SerializeObjectToXml<T>(T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            string result;

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, objectToSerialize);
                    result = stringWriter.ToString(); // Your XML
                }
            }

            return result;
        }

        public T DeserializeObjestFromXml<T>(string xmlToDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            T result;

            using (TextReader textReader = new StringReader(xmlToDeserialize))
            {
                result = (T)xmlSerializer.Deserialize(textReader);
            }

            return result;
        }

    }
}