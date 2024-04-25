using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DataTransferObjects
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required , Range(1,99)]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public string BrandName { get; set; }
    }
}
