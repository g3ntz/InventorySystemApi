using System;
using System.Collections.Generic;

namespace InventorySystem.Core.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public double Price { get; set; }

        public List<Product> Products { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
