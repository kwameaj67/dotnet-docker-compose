using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Innoloft_Test.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        [Required]  //we can't create an event without a title.
        public string? title { get; set; }
        public string? description { get; set; }
        public string? location { get; set; }
        public string? event_type { get; set; }
        public DateTimeOffset? start_date { get; set; }
        public DateTimeOffset? end_date { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public DateTimeOffset? updated_at { get; set; }

        public Guid event_creator_id { get; set; } = Guid.Empty;

    }
}
