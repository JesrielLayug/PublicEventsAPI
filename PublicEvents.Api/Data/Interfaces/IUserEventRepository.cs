using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Data.Interfaces
{
    public interface IUserEventRepository
    {
        Task<UserEvent?> GetUserEventByIdAsync(Guid id);
        Task<Guid> CreateUserEventAsync(UserEvent userEvent);
        Task<ICollection<UserEvent>> GetAllUserEventAsync();
        Task<bool> DeleteUserEventByEventAsync(Guid eventId);
        Task<bool> DeleteUserEventByUserAsync(Guid userId);
    }
}
