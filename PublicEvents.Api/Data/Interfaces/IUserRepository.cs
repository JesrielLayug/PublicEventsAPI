using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> IsTheUserExistAsync(Guid id);
        Task<bool> CredentialsExistAsync (User user);
        Task<Guid> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
