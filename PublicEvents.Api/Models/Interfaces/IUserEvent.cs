using PublicEvents.Api.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.Interfaces
{
    public interface IUserEvent
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }
}
