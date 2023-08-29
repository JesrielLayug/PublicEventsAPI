using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.Domain
{
    public class UserEvent : IUserEvent
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid EventId { get; set; }
    }
}
