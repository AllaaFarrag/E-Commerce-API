namespace E_Commerce.Core.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }  //Referene navigational property

        public ProductType ProductType { get; set; }    //Referene navigational property
        public int TypeId { get; set; }
    }
}
