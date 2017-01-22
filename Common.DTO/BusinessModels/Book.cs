using System;
using Common.DTO.AmazonModels;

namespace Common.DTO.BusinessModels
{
    public class Book : Item<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }

        public override Book BuildItemFromAmazonItem(Item item)
        {
            var book = new Book();

            book.Author = item.ItemAttributes.Author;
            book.Title = item.ItemAttributes.Title;
            book.Url = item.DetailPageURL;
            book.Isbn = item.ItemAttributes.ISBN;

            if (item.ItemAttributes.ListPrice != null)
                book.Price = decimal.Parse(item.ItemAttributes.ListPrice.Amount);
            else if (item.OfferSummary?.LowestNewPrice?.Amount != null)
                book.Price = decimal.Parse(item.OfferSummary.LowestNewPrice.Amount);

            return book;
        }
    }
}
