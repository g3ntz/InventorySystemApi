namespace InventorySystem.Core.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
