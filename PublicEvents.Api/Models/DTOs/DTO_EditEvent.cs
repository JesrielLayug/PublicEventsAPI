using PublicEvents.Api.Models.Interfaces;

namespace PublicEvents.Api.Models.DTOs
{
    public class DTO_EditEvent : IEvent
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
