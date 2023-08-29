using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Service.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _uer;

        public UserEventService(IUserEventRepository uer)
        {
            this._uer = uer;
        }
        public async Task<Guid?> CreateUserEventAsync(Guid userId, Guid eventId)
        {
            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId,
            };

            return await _uer.CreateUserEventAsync(userEvent);
        }

        public async Task<bool> DeleteAnUserEventByEventAsync(Guid eventId)
        {
            var deleted = await _uer.DeleteUserEventByEventAsync(eventId);
            return deleted;
        }

        public async Task<bool> DeleteAnUserEventByUserAsync(Guid userId)
        {
            var deleted = await _uer.DeleteUserEventByUserAsync(userId);
            return deleted;
        }

        public async Task<ICollection<DTO_UserEvent>> GetUserEventAsync()
        {
            var userEventDomain = await _uer.GetAllUserEventAsync();
            var dto_userEvents = new List<DTO_UserEvent>();
            foreach(var userEvent in userEventDomain)
            {
                dto_userEvents.Add(new DTO_UserEvent
                {
                    Id = userEvent.Id,
                    UserId = userEvent.UserId,
                    EventId = userEvent.EventId,
                });
            }

            return dto_userEvents;
        }

        public async Task<UserEvent?> GetUserEventByIdAsync(Guid id)
        {
            var userEvent = await _uer.GetUserEventByIdAsync(id);
            return userEvent;
        }
    }
}
