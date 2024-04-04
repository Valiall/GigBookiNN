using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigBookin.Models.Entities
{
    public class EventPerformer
    {
       

        [ForeignKey(nameof(Performer))]
        public Guid PerformerId { get; set; }
        public Performer Performer { get; set; } = null!;

        [ForeignKey(nameof(EventOrganiser))]
        public Guid EventOrganiserId { get; set; }
        public EventOrganiser EventOrganiser { get; set; } = null!;
    }
}
