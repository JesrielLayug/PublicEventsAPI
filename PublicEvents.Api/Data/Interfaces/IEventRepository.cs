using PublicEvents.Api.Models.Domain;

namespace PublicEvents.Api.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(Guid? id);
        Task<Guid> CreateEventAsync(Event @event);
        Task<bool> DeleteEventAsync(Event @event);
        Task<bool> UpdateEventAsync(Event @event);
        Task<bool> IsEventAlreadyExistAsync(Event @event);
    }
}
