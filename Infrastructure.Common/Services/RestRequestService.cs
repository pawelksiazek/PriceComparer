using System;
using System.Net;
using System.Xml.Serialization;

namespace Infrastructure.Common.Services
{
    public class RestRequestService
    {

        public T Get<T>(string url, string namespce = null)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();

                var xmlSerializer = new XmlSerializer(typeof(T), namespce);
                var result = (T)xmlSerializer.Deserialize(response.GetResponseStream());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception: " + e.Message);
                Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return default(T);
        }
    }
}