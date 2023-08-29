using PublicEvents.Api.Data.Interfaces;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Models.Interfaces;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _ur;
        private readonly IUserEventService _ues;

        public UserService(IUserRepository ur, IUserEventService ues)
        {
            this._ur = ur;
            this._ues = ues;
        }
        public async Task<Guid> CreateUserAsync(DTO_AddUser addUserRequest)
        {
            var domainUser = new User
            {
                FirstName = addUserRequest.FirstName,
                Lastname = addUserRequest.Lastname,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password,
            };

            bool credentialsExist = await _ur.CredentialsExistAsync(domainUser);
            if(!credentialsExist)
            {
                throw new UnauthorizedAccessException("User is already exist");
            }

            return await _ur.CreateUserAsync(domainUser);
        }


        public async Task<ICollection<DTO_User>> GetAllUsersAsync()
        {
            IEnumerable<User> users = await _ur.GetAllUsersAsync();
            ICollection<DTO_User> dto_users = new List<DTO_User>();
            foreach(var user in users) 
            {
                dto_users.Add(new DTO_User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Password = user.Password,
                });
            }

            return dto_users;
        }

        public async Task<DTO_User?> GetUserByIdAsync(Guid id)
        {
            User? userDomain = await _ur.GetUserByIdAsync(id);
            if (userDomain == null)
                return null;

            DTO_User dto_user = new DTO_User
            {
                Id = userDomain.Id,
                FirstName = userDomain.FirstName,
                Lastname = userDomain.Lastname,
                Email = userDomain.Email,
                Password = userDomain.Password,
            };

            return dto_user;
        }

        public async Task<bool> UpdateUserAsync(Guid userId, DTO_EditUser user)
        {
            var existingUser = await _ur.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                return false;
            }


            existingUser.FirstName = user.FirstName;
            existingUser.Lastname = user.Lastname;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            bool tobeUpdatedUser = await _ur.UpdateUserAsync(existingUser);
            if (!tobeUpdatedUser)
            {
                return false;
            }
            return tobeUpdatedUser;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var existingUser = await _ur.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            var deletedEvent = await _ur.DeleteUserAsync(existingUser);
            var deletedUserEvent = await _ues.DeleteAnUserEventByUserAsync(id);
            return deletedEvent && deletedUserEvent;
        }
    }
}
