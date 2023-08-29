using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.Domain
{
    public class User : IUser
    {
        [Key]
        [Required]
        public Guid Id { get ; set ; }
        [Required]
        public string FirstName { get; set ; }
        [Required]
        public string Lastname { get ; set ; }
        [Required]
        public string Email { get; set ; }
        [Required]
        public string Password { get; set ; }
    }
}
