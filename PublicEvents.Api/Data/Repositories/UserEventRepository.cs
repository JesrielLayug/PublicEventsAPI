using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Data.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly AppDbContext _db;

        public UserEventRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<Guid> CreateUserEventAsync(UserEvent userEvent)
        {
            _db.AddAsync(userEvent);
            await _db.SaveChangesAsync();

            return userEvent.UserId;
        }

        public async Task<bool> DeleteUserEventByEventAsync(Guid eventId)
        {
            UserEvent? @event = await _db.UserEvents.FirstOrDefaultAsync(ue => ue.EventId == eventId);
            if(@event == null)
            {
                return false;
            }

            _db.UserEvents.Remove(@event);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> DeleteUserEventByUserAsync(Guid userId)
        {
            UserEvent? @user = await _db.UserEvents.FirstOrDefaultAsync(ue => ue.UserId == userId);
            if (@user == null)
            {
                return false;
            }

            _db.UserEvents.Remove(@user);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<ICollection<UserEvent>> GetAllUserEventAsync()
        {
            ICollection<UserEvent> userEvents = await _db.UserEvents.ToListAsync();
            return userEvents;
        }

        public async Task<UserEvent?> GetUserEventByIdAsync(Guid id)
        {
            UserEvent? userEvent = await _db.UserEvents.FirstOrDefaultAsync(ue => ue.Id == id);
            if (userEvent == null)
                return null;

            return userEvent;
        }
    }
}
