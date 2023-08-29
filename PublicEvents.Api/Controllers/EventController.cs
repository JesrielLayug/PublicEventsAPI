using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PublicEvents.Api.Data;
using PublicEvents.Api.Data.Repositories;
using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.DTOs;
using PublicEvents.Api.Service.Interfaces;

namespace PublicEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _es;
        private readonly IUserEventService _ues;
        private readonly IUserService _us;

        public EventController(IEventService es, IUserEventService ues, IUserService us)
        {
            this._es = es;
            this._ues = ues;
            this._us = us;
        }

        [HttpGet("AllEvents")]
        public async Task<IActionResult> GetAll()
        {
            ICollection<DTO_Event> events = await _es.GetEventsAsync();
            return Ok(events);
        }

        [HttpGet("GetById/{EventId}")]
        public async Task<IActionResult> GetEventById(Guid EventId)
        {
            var @event = await _es.GetEventByIdAsync(EventId);
            if (@event == null)
            {
                return NotFound("Event does not exist.");
            }

            return Ok(@event);
        }

        [HttpPost("Create/{OrganizerId}")]
        public async Task<IActionResult> CreateEvent(Guid OrganizerId, [FromBody] DTO_AddEvent addEventRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validOrganizer = await _us.GetUserByIdAsync(OrganizerId);
            if(validOrganizer == null)
            {
                return NotFound("User is not authorized to create an event.");
            }

            var eventId = await _es.CreateEventAsync(OrganizerId, addEventRequest);
            var @event = await _es.GetEventByIdAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var ue_userId = eventId;
            var ue_eventId = @event.Id;
            await _ues.CreateUserEventAsync(ue_userId, ue_eventId);

            return CreatedAtAction(nameof(GetEventById), new { EventId = eventId }, @event);
        }

        [HttpPut("Update/{EventId}")]
        public async Task<IActionResult> UpdateEvent(Guid EventId, [FromBody] DTO_EditEvent editEventRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventUpdate = await _es.UpdateEventAsync(EventId, editEventRequest);
            if (!eventUpdate)
            {
                return BadRequest("Unable to update the event");
            }
            return Ok("Successfully updated the event.");
        }

        [HttpDelete("Delete/{EventId}")]
        public async Task<IActionResult> DeleteEventById(Guid EventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool deleteAnEvent = await _es.DeleteAnEventAsync(EventId);
            if (!deleteAnEvent)
            {
                return NotFound("Event does not exist.");
            }

            return NoContent();
        }
    }
}
