using GigBookin.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigBookin.Models
{
    public class RemoveEventViewModel
    {
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


        public Guid PerformerId { get; set; }
        public Performer Performer { get; set; }


    }
}
