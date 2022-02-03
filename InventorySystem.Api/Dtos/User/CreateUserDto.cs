namespace InventorySystem.Api.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public int UserStatusId { get; set; }
        public int RoleId { get; set; }
    }
}
