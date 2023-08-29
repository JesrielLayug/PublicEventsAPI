using Microsoft.EntityFrameworkCore;
using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;

namespace PublicEvents.Api.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private AppDbContext _db;

        public EventRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            IEnumerable<Event> events = await _db.Events.ToListAsync();
            return events;
        }

        public async Task<Event?> GetEventByIdAsync(Guid? id)
        {
            Event? @event = await _db.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (@event == null)
                return null;

            return @event;
        }

        public async Task<Guid> CreateEventAsync(Event @event)
        {
            _db.Events.AddAsync(@event);
            await _db.SaveChangesAsync();
            return @event.Id;
        }

        public async Task<bool> DeleteEventAsync(Event @event)
        {
            _db.Events.Remove(@event);
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> UpdateEventAsync(Event @event)
        {
            _db.Events.Update(@event);
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsEventAlreadyExistAsync(Event @event)
        {
            var existingEvent = await _db.Events.FirstOrDefaultAsync(e => e.Title == @event.Title 
                                                                       && e.StartDate == @event.StartDate
                                                                       && e.EndDate == @event.EndDate);

            if(existingEvent != null)
            {
                return false;
            }

            return true;
        }
    }
}
