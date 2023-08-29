using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.DTOs
{
    public class DTO_AddUser : IUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
