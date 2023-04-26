using Innoloft_Test.Models;

namespace Innoloft_Test.Responses
{
    public class EventResponse
    {
        public int pages { get; set; } 
        public int current_page { get; set; }  
        public IEnumerable<Event> events { get; set; } // = Enumerable<Event>.Empty;
    }

    public class SingleEventResponse
    {
        public Event event_activity { get; set; }
        public User user { get; set; }
    }
}
