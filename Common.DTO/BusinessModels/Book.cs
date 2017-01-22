using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.DTO.AmazonModels;

namespace Common.DTO.BusinessModels
{
    public class Book : Item<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Ean { get; set; }

        public override Book BuildBusinessItemFromAmazonItem(Item item)
        {
            var book = new Book();

            book.Author = item.ItemAttributes.Author;
            book.Title = item.ItemAttributes.Title;
            book.Url = item.DetailPageURL;
            book.Isbn = item.ItemAttributes.ISBN;
            book.Ean = item.ItemAttributes.EAN;

            if (item.Offers?.Offer?.First().OfferListing?.First().Price != null)
                book.Price = decimal.Parse(item.Offers.Offer.First().OfferListing.First().Price.FormattedPrice, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"));

            return book;
        }

        public override Book BuildBusinessItemFromApressItem(Dictionary<string,string> item)
        {
            var book = new Book();

            if (item.ContainsKey("Author")) book.Author = item["Author"];
            if (item.ContainsKey("Title")) book.Title = item["Title"];

            book.Price = decimal.Parse(item["Price"], NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"));
            book.Url = item["Url"];

            return book;
        }
    }
}
