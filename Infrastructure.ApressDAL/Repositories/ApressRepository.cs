using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.DTO.BusinessModels;
using HtmlAgilityPack;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.ApressDAL.Repositories
{
    public class ApressRepository<T> : IItemsRepository<T> where T : Item<T>, new()
    {
        public List<T> SearchItemsByName(string itemName)
        {
            // TODO: Implement Apress search

            return new List<T>();
        }

        public T GetItemById(string itemId)
        {
            string htmlCode = null;
            string url = string.Format("{0}{1}", "http://www.apress.com/us/book/", itemId);

            try
            {
                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(url);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Dictionary<string, string> apressItem = new Dictionary<string, string>
            {
                { "Url", url}
            };

            T itemFound = null;

            if (htmlCode != null)
            {
                var document = new HtmlDocument();
                document.LoadHtml(htmlCode);

                var rows = document.DocumentNode.SelectNodes("//span[@class='price-box']/span[@class='price']");

                if (rows != null)
                {
                    Func<Dictionary<string, string>, T> buildBusinessItemFromApressItem = new T().BuildBusinessItemFromApressItem;
                    apressItem["Price"] = rows.First().InnerText;
                    itemFound = buildBusinessItemFromApressItem(apressItem);
                }
            }

            return itemFound;
        }
    }
}
