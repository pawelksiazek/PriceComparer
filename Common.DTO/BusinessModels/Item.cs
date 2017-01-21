using Common.DTO.AmazonModels;

namespace Common.DTO.BusinessModels
{
    public abstract class Item<T>
    {
        public decimal Price { get; set; }
        public string Url { get; set; }

        public abstract T BuildItemFromLookup(ItemLookupResponse itemLookupResponse);
    }
}
