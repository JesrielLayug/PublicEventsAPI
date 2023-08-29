using PublicEvents.Api.Models.Interfaces;

namespace PublicEvents.Api.Models.DTOs
{
    public class DTO_EditUser : IUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
