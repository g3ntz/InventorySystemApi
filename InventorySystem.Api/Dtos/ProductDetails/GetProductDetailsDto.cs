using InventorySystem.Core.Models;
using System;

namespace InventorySystem.Api.Dtos.ProductDetails
{
    public class GetProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public double Price { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; }
    }
}
