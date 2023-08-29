using PublicEvents.Api.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PublicEvents.Api.Models.Domain
{
    public class Event : IEvent
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set ; }
        [Required]
        public string Description { get ; set ; }
        [Required]
        public string Location { get ; set ; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid OrganizerId { get; set; }
    }
}
