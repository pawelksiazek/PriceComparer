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
                using (var client = new WebClient())
                {
                    htmlCode = client.DownloadString(url);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            T itemFound = null;

            if (htmlCode != null)
            {
                var document = new HtmlDocument();
                document.LoadHtml(htmlCode);

                var apressItem = new Dictionary<string, string>();
                apressItem["Url"] = url;

                var author = document.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[1]/div[1]/div[1]/div[2]/p/strong");
                if (author != null) apressItem["Author"] = author.First().InnerText;

                var title = document.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[1]/div[1]/div[1]/div[2]/h1");
                if (title != null) apressItem["Title"] = title.First().InnerText;

                var price = document.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div[2]/div[1]/div/dl/dt[2]/span[2]/span"); //"//span[@class='price-box']/span[@class='price']"
                if (price != null) apressItem["Price"] = price.First().InnerText;

                Func<Dictionary<string, string>, T> buildBusinessItemFromApressItem = new T().BuildBusinessItemFromApressItem;
                itemFound = buildBusinessItemFromApressItem(apressItem);
            }

            return itemFound;
        }
    }
}
