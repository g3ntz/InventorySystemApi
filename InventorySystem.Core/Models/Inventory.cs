using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Core.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string CancelReason { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }


        [ForeignKey("IssuerId")]
        public User Issuer { get; set; }

        public int InventoryStatusId { get; set; }
        public InventoryStatus InventoryStatus { get; set; }

        public int? RequestId { get; set; }
        public Request Request { get; set; }
    }
}
