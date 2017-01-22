﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

            //if (item.OfferSummary?.LowestNewPrice?.Amount != null)
            //    book.Price = decimal.Parse(item.OfferSummary.LowestNewPrice.FormattedPrice, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"));
            //else 
            if (item.ItemAttributes.ListPrice != null)
                book.Price = decimal.Parse(item.ItemAttributes.ListPrice.FormattedPrice, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"));

            return book;
        }

        public override Book BuildBusinessItemFromApressItem(Dictionary<string,string> item)
        {
            var book = new Book();
            book.Url = item["Url"];
            book.Price = decimal.Parse(item["Price"], NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-US"));
            return book;
        }
    }
}
