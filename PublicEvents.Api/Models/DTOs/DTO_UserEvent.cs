using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.DTOs
{
    public class DTO_UserEvent : IUserEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
