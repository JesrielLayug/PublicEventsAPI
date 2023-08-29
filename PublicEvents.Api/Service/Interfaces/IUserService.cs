using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<DTO_User>> GetAllUsersAsync();
        Task<DTO_User?> GetUserByIdAsync (Guid id);
        Task<Guid> CreateUserAsync(DTO_AddUser addUserRequest);
        Task<bool> UpdateUserAsync(Guid userId, DTO_EditUser user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
