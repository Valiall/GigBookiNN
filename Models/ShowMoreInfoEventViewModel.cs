using GigBookin.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigBookin.Models
{
    public class ShowMoreInfoEventViewModel
    {
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
