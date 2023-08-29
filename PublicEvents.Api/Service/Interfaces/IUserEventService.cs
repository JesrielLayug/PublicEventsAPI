using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;

namespace PublicEvents.Api.Service.Interfaces
{
    public interface IUserEventService
    {
        Task<UserEvent?> GetUserEventByIdAsync(Guid id);
        Task<Guid?> CreateUserEventAsync(Guid userId, Guid eventId);
        Task<ICollection<DTO_UserEvent>> GetUserEventAsync(); 
        Task<bool> DeleteAnUserEventByEventAsync(Guid eventId);
        Task<bool> DeleteAnUserEventByUserAsync(Guid userId);
    }
}
