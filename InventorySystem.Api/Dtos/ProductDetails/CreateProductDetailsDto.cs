using System;

namespace InventorySystem.Api.Dtos.ProductDetails
{
    public class CreateProductDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public double Price { get; set; }

        public int ProductCategoryId { get; set; }
        public int OwnerId { get; set; }
    }
}
