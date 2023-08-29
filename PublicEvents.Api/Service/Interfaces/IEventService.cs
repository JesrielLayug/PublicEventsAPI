

using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Service.Interfaces
{
    public interface IEventService
    {
        Task<ICollection<DTO_Event>> GetEventsAsync();
        Task<DTO_Event?> GetEventByIdAsync(Guid? id);
        Task<Guid> CreateEventAsync(Guid organizerId, DTO_AddEvent addEventRequest);
        Task<bool> DeleteAnEventAsync(Guid id);
        Task<bool> UpdateEventAsync(Guid eventId, DTO_EditEvent @event);
    }
}
