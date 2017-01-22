using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.ApressDAL.Repositories
{
    public class ApressRepository<T> : IItemsRepository<T>
    {

        //var homePageRequest = new RestRequest("https://secure.getinbank.pl/accounts/", Method.GET);
        //var homePageResult = this.requestHelper.Execute(homePageRequest);

        //var document = new HtmlDocument();
        //document.LoadHtml(homePageResult.Content);

        //    var rows = document.DocumentNode.SelectNodes("//span[@class='price-box']/span[@class='price']");
        //    if (rows == null)
        //    {
        //        this.requestHelper.DebugLastResponse();
        //        throw new Exception(homePageResult.Content);
        //}

        //    foreach (var row in rows)
        //    {
        //        row.InnerText

        //}

        public List<T> SearchItemsByName(string itemName)
        {
            throw new NotImplementedException();
        }

        public T GetItemById(string itemId)
        {
            throw new NotImplementedException();
        }
    }
}
