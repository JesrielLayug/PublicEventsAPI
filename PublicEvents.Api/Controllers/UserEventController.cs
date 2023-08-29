using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventController : ControllerBase
    {
        private readonly IUserEventService _ues;

        public UserEventController(IUserEventService ues) 
        {
            this._ues = ues;
        }

        [HttpGet("AllUserEvents")]
        public async Task<IActionResult> GetUserEvent()
        {
            var @events = await _ues.GetUserEventAsync();
            return Ok(events);
        }

        [HttpGet("GetById/{EventId}")]
        public async Task<IActionResult> GetUserEventById([FromRoute] Guid EventId)
        {
            UserEvent? domain_userEvent = await _ues.GetUserEventByIdAsync(EventId);
            if (domain_userEvent == null)
            {
                return NotFound();
            }

            var dto_userEvent = new DTO_UserEvent
            {
                Id = domain_userEvent.Id,
                UserId = domain_userEvent.UserId,
                EventId = domain_userEvent.EventId,
            };

            return Ok(dto_userEvent);
        }
    }
}
