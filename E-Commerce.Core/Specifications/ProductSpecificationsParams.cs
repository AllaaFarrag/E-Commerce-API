using System.Runtime;

namespace E_Commerce.Core.Specifications
{
    public class ProductSpecificationsParams
    {
        private const int MAXPAGESIZE = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingParams? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value; }
        }
        

        private string? _search;

        public string? Search
        {
            get => _search;
            set => _search = value?.Trim().ToLower();
        }


    }
}
