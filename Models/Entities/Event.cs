using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigBookin.Models.Entities
{
    public class Event
    {
        public Event()
        {
           Id= Guid.NewGuid();
          
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }
        
        
        
        [Required]
        public int WorkingHours { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }


        [ForeignKey("Performer")]
        public Guid PerformerId { get; set; }
        public Performer Performer{ get; set; }
      
        
        public ICollection<EventPerformer> EventPerformers { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();



    }
}
