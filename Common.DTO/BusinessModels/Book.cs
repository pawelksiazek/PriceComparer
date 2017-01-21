using Common.DTO.AmazonModels;

namespace Common.DTO.BusinessModels
{
    public class Book : Item<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Isbn { get; set; }

        public override Book BuildItemFromLookup(ItemLookupResponse itemLookupResponse)
        {
            throw new System.NotImplementedException();
        }
    }
}
