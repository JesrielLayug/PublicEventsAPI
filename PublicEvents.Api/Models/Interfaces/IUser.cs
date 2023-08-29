using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.Interfaces
{
    public interface IUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
