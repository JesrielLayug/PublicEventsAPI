using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _us;

        public UserController(IUserService us)
        {
            this._us = us;
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            ICollection<DTO_User> dto_users = await _us.GetAllUsersAsync();
            return Ok(dto_users);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid UserId)
        {
            var @user = await _us.GetUserByIdAsync(UserId);
            if (@user == null)
            {
                return BadRequest("User does not exist.");
            }

            return Ok(@user);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] DTO_AddUser addUserRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _us.CreateUserAsync(addUserRequest);
            var newUser = await _us.GetUserByIdAsync(userId);
            if(newUser == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetUserById), new { UserId = newUser.Id }, newUser);
        }

        [HttpPut("Update/{UserId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] DTO_EditUser dto_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateUser = await _us.UpdateUserAsync(userId, dto_User);
            if (!updateUser)
            {
                return BadRequest("Unable to edit your user account");
            }
            return Ok("Successfully updated your user account");
        }

        [HttpDelete("Delete/{UserId}")]
        public async Task<IActionResult> DeleteUser(Guid UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteUser = await _us.DeleteUserAsync(UserId);
            if (!deleteUser)
            {
                return NotFound();
            }

            return NoContent(); 
        }
    }
}
