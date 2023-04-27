using System.ComponentModel.DataAnnotations;

namespace Innoloft_Test.Models
{
    public class EventParticipation
    {
        public Guid id { get; set; } = Guid.Empty;
        public Guid event_id { get; set; } = Guid.Empty;
        public Guid participant_id { get; set; }
    }

    public class EventInvitation
    {
        public Guid id { get; set; }
        public Guid event_id { get; set;}
        public Guid participant_id { get; set;}
        public bool is_approved = false;
    } 
}
