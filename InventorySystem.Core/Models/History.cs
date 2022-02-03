using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Core.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime UpdatedDate{ get; set; }

        //public int InventoryStatusId { get; set; }
        [ForeignKey("InventoryStatusId")]
        public InventoryStatus InventoryStatus { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        //public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
