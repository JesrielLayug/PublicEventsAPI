using Microsoft.EntityFrameworkCore;
using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;

namespace PublicEvents.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            ICollection<User> users = await _db.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> IsTheUserExistAsync(Guid id)
        {
            User? organizer = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(organizer == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CredentialsExistAsync(User user)
        {
            var credentialsExist = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.FirstName == user.FirstName);
            if(credentialsExist != null)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            IEnumerable<User> users = await _db.Users.ToListAsync();
            return users;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _db.Users.Remove(user);
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }
    }
}
