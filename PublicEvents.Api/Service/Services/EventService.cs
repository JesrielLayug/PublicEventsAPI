using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Service.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _er;
        private readonly IUserRepository _ur;
        private readonly IUserEventService _ues;

        public EventService(IEventRepository er, IUserRepository ur, IUserEventService ues) 
        {
            this._er = er;
            this._ur = ur;
            this._ues = ues;
        }
        public async Task<ICollection<DTO_Event>> GetEventsAsync()
        {
            IEnumerable<Event> eventDomain = await _er.GetEventsAsync();
            ICollection<DTO_Event> DTO_events = new List<DTO_Event>();
            foreach(var item in  eventDomain)
            {
                DTO_events.Add(new DTO_Event()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Location = item.Location,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    CreatedDate = item.CreatedDate,
                    OrganizerId = item.OrganizerId,
                });
            }

            return DTO_events;
        }

        public async Task<DTO_Event?> GetEventByIdAsync(Guid? id)
        {
            Event? eventDomain = await _er.GetEventByIdAsync(id);
            if (eventDomain == null)
            {
                return null;
            }

            DTO_Event DTO_event = new DTO_Event()
            {
                Id = eventDomain.Id,
                Title = eventDomain.Title,
                Description = eventDomain.Description,
                Location = eventDomain.Location,
                StartDate = eventDomain.StartDate,
                EndDate = eventDomain.EndDate,
                CreatedDate = eventDomain.CreatedDate,
                OrganizerId = eventDomain.OrganizerId,
            };

            return DTO_event;
        }

        public async Task<Guid> CreateEventAsync(Guid OrganizerId, DTO_AddEvent addEventRequest)
        {

            var eventDomain = new Event
            {
                Title = addEventRequest.Title,
                Description = addEventRequest.Description,
                Location = addEventRequest.Location,
                StartDate = addEventRequest.StartDate,
                EndDate = addEventRequest.EndDate,
                CreatedDate = addEventRequest.CreatedDate,
                OrganizerId = OrganizerId,
            };

            bool isEventExisting = await _er.IsEventAlreadyExistAsync(eventDomain);
            if (!isEventExisting)
            {
                throw new UnauthorizedAccessException("Event is already exist");
            }

            return await _er.CreateEventAsync(eventDomain);
        }

        public async Task<bool> DeleteAnEventAsync(Guid id)
        {
            var eventExist = await _er.GetEventByIdAsync(id);
            if (eventExist == null)
            {
                return false;
            }

            var deletedEvent = await _er.DeleteEventAsync(eventExist);
            var deletedUserEvent = await _ues.DeleteAnUserEventByEventAsync(id);
            return deletedEvent && deletedUserEvent;
        }

        public async Task<bool> UpdateEventAsync(Guid eventId, DTO_EditEvent @event)
        {
            var existingEvent = await _er.GetEventByIdAsync(@eventId);
            if (existingEvent == null)
            {
                return false;
            }

            existingEvent.Title = @event.Title;
            existingEvent.Description = @event.Description;
            existingEvent.Location = @event.Location;
            existingEvent.CreatedDate = @event.CreatedDate;
            existingEvent.StartDate = @event.StartDate;
            existingEvent.EndDate = @event.EndDate;

            var successfullUpdate = await _er.UpdateEventAsync(existingEvent);
            if (!successfullUpdate)
            {
                return false;
            }
            return successfullUpdate;
        }
    }
}
