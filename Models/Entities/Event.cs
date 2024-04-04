using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigBookin.Models.Entities
{
    public class Event
    {
        public Event()
        {
           Id= Guid.NewGuid();
           this.RequestIsAccepted = false;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        [Required]
        public int WorkingHours { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }


        [ForeignKey("EventOrganiser")]
        public Guid EventOrganiserId { get; set; }
        public EventOrganiser EventOrganiser { get; set; }

        [Required]
        public bool RequestIsAccepted { get; set; }


    }
}
