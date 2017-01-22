using System;
using System.Collections.Generic;
using System.Linq;
using Common.DTO.AmazonModels;
using Common.DTO.BusinessModels;
using Infrastructure.AmazonDAL.Services;
using Infrastructure.Common.Interfaces;
using Infrastructure.Common.Services;

namespace Infrastructure.AmazonDAL.Repositories
{
    public class AmazonRepository<T> : IItemsRepository<T> where T : Item<T>, new()
    {
        private readonly RestRequestService _restRequestService;

        private const string MyAwsAccessKeyId = "AKIAICTYQILKZ6BXY5JQ"; //"YOUR_AWS_ACCESS_KEY_ID";
        private const string MyAwsSecretKey = "W4QdgzSPcqzu2x26Deg2hEIhqS7V2p3bzlzXSWVR"; //"YOUR_AWS_SECRET_KEY";
        private const string Destination = "ecs.amazonaws.com";

        private const string Namespace = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";

        public AmazonRepository()
        {
            _restRequestService = new RestRequestService();
        }

        public List<T> SearchItemsByName(string itemName)
        {
            var requestUrlProvider = new SignedRequestProvider(MyAwsAccessKeyId, MyAwsSecretKey, Destination);

            IDictionary<string, string> requestData = new Dictionary<string, string>();
            requestData["Service"] = "AWSECommerceService";
            requestData["Version"] = "2011-08-01";
            requestData["AssociateTag"] = "momenthoughts-20";
            requestData["Operation"] = "ItemSearch";
            requestData["SearchIndex"] = "Books";
            requestData["Keywords"] = itemName;
            requestData["ResponseGroup"] = "ItemAttributes";

            var requestUrl = requestUrlProvider.Sign(requestData);
            var searchResponse = _restRequestService.Get<ItemSearchResponse>(requestUrl, Namespace);

            Func<Item, T> buildItemFromAmazonItem = new T().BuildItemFromAmazonItem;
            var itemsFound = new List<T>();

            if (searchResponse.Items.First().Item != null)
            {
                itemsFound = searchResponse.Items.First().Item.Select(amazonItem => buildItemFromAmazonItem(amazonItem)).ToList();
            }
            return itemsFound;
        }

        public T GetItemById(string itemId)
        {
            var requestUrlProvider = new SignedRequestProvider(MyAwsAccessKeyId, MyAwsSecretKey, Destination);

            IDictionary<string, string> requestData = new Dictionary<string, string>();
            requestData["Service"] = "AWSECommerceService";
            requestData["Version"] = "2011-08-01";
            requestData["AssociateTag"] = "momenthoughts-20";
            requestData["Operation"] = "ItemLookup";
            requestData["SearchIndex"] = "Books";
            requestData["IdType"] = "ISBN"; // ASIN
            requestData["ItemId"] = itemId.ToString();
            requestData["ResponseGroup"] = "ItemAttributes,Offers";

            var requestUrl = requestUrlProvider.Sign(requestData);
            //var title = FetchTitle(requestUrl);
            var lookupResponse = _restRequestService.Get<ItemLookupResponse>(requestUrl);

            Func<Item, T> buildItemFromAmazonItem = new T().BuildItemFromAmazonItem;
            T itemFound = null;

            if (lookupResponse != null)
            {
                itemFound = buildItemFromAmazonItem(lookupResponse.Items.First());
            }
            return itemFound;
        }
    }
}
