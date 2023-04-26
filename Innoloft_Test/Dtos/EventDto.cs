using Innoloft_Test.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft_Test.Dtos
{
    public class EventDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? location { get; set; }
        public string? event_type { get; set; }
        public DateTimeOffset? start_date { get; set; }
        public DateTimeOffset? end_date { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public int event_creator_id { get; set; }
    }
}
