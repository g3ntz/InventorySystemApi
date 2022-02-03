using InventorySystem.Core.Models;

namespace InventorySystem.Api.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public int UserStatusId { get; set; }
        public UserStatus UserStatus { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
