using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Common.DTO.AmazonModels;
using Infrastructure.AmazonDAL.Services;
using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Services;

namespace Infrastructure.AmazonDAL.Repositories
{
    public class AmazonRepository<T> : IItemsRepository<T>
    {
        private readonly IRestRequestService _restRequestService;

        private const string MyAwsAccessKeyId = "AKIAICTYQILKZ6BXY5JQ"; //"YOUR_AWS_ACCESS_KEY_ID";
        private const string MyAwsSecretKey = "W4QdgzSPcqzu2x26Deg2hEIhqS7V2p3bzlzXSWVR"; //"YOUR_AWS_SECRET_KEY";
        private const string Destination = "ecs.amazonaws.com";

        private const string Namespace = "http://webservices.amazon.com/AWSECommerceService/2009-03-31";

        public AmazonRepository()
        {
            _restRequestService = new RestRequestService();
        }

        public List<T> SearchItemsByName(string itemName)
        {
            SignedRequestProvider requestUrlProvider = new SignedRequestProvider(MyAwsAccessKeyId, MyAwsSecretKey, Destination);

            IDictionary<string, string> requestData = new Dictionary<string, string>();
            requestData["Service"] = "AWSECommerceService";
            requestData["Version"] = "2011-08-01";
            requestData["Operation"] = "ItemSearch";
            requestData["SearchIndex"] = "Books";
            requestData["Keywords"] = itemName;
            requestData["ResponseGroup"] = "ItemAttributes";

            var requestUrl = requestUrlProvider.Sign(requestData);

            var items = _restRequestService.Get<ItemSearchResponse>(requestUrl);

            return items;
        }

        public T GetItemById(int itemId)
        {
            SignedRequestProvider requestUrlProvider = new SignedRequestProvider(MyAwsAccessKeyId, MyAwsSecretKey, Destination);

            IDictionary<string, string> requestData = new Dictionary<string, string>();
            requestData["Service"] = "AWSECommerceService";
            requestData["Version"] = "2011-08-01";
            requestData["Operation"] = "ItemLookup";
            requestData["SearchIndex"] = "Books";
            requestData["IdType"] = "ISBN"; // ASIN
            requestData["ItemId"] = itemId.ToString();
            requestData["ResponseGroup"] = "ItemAttributes,Offers";

            var requestUrl = requestUrlProvider.Sign(requestData);

            //var title = FetchTitle(requestUrl);
            var item = _restRequestService.Get<ItemLookupResponse>(requestUrl);
            return item;

        }




        private static string FetchTitle(string url)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", Namespace);
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    String message = errorMessageNodes.Item(0).InnerText;
                    return "Error: " + message + " (but signature worked)";
                }

                XmlNode titleNode = doc.GetElementsByTagName("Title", Namespace).Item(0);
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
