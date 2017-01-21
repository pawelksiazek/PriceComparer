using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Infrastructure.AmazonDAL.Services;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.AmazonDAL.Repositories
{
    public class AmazonRepository<T> : IItemsRepository<T>
    {
        private const string MY_AWS_ACCESS_KEY_ID = "YOUR_AWS_ACCESS_KEY_ID";
        private const string MY_AWS_SECRET_KEY = "YOUR_AWS_SECRET_KEY";
        private const string DESTINATION = "ecs.amazonaws.com";

        private const string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2009-03-31";

        public List<T> SearchItemsByName(string itemName)
        {
            throw new System.NotImplementedException();
        }

        public T GetItemById(int itemId)
        {
            SignedRequestProvider helper = new SignedRequestProvider(MY_AWS_ACCESS_KEY_ID, MY_AWS_SECRET_KEY, DESTINATION);

            IDictionary<string, string> r1 = new Dictionary<string, string>();
            r1["Service"] = "AWSECommerceService";
            r1["Version"] = "2009-03-31";
            r1["Operation"] = "ItemLookup";
            r1["ItemId"] = itemId.ToString();
            r1["ResponseGroup"] = "Small";

            var requestUrl = helper.Sign(r1);

            var title = FetchTitle(requestUrl);


        }

        private static string FetchTitle(string url)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    String message = errorMessageNodes.Item(0).InnerText;
                    return "Error: " + message + " (but signature worked)";
                }

                XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(0);
                string title = titleNode.InnerText;
                return title;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Caught Exception: " + e.Message);
                System.Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return null;
        }
    }
}
