using PublicEvents.Api.Models.Domain;
using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicEvents.Api.Models.DTOs
{
    public class DTO_Event : IEvent
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid OrganizerId { get; set; }
    }
}
