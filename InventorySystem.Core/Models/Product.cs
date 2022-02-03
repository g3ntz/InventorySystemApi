namespace InventorySystem.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int ProductDetailsId { get; set; }
        public ProductDetails ProductDetails { get; set; }

        public int ProductStatusId { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
