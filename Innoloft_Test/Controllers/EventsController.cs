
using Innoloft_Test.Data;
using Innoloft_Test.Interfaces;
using Innoloft_Test.Models;
using Innoloft_Test.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Innoloft_Test.Controllers
{
    [Route("api")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEvent _eventRepo;
        private readonly IUser _user;
        private readonly AppDbContext _context;
        public EventsController(IEvent eventRepository, IUser user, AppDbContext context)
        {
            _eventRepo = eventRepository;
            _user = user;
            _context = context;
        }

        #region events
        [HttpGet("events")]
        [SwaggerOperation(Summary = "returns a list of entire events in pages")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromQuery]int page = 1)
        {
            var pageResults = 10f;
            
            var pageCount = Math.Ceiling(_context.events.Count() / pageResults);

            var events = (await _eventRepo.GetAllEvents())
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults);

            var response = new EventResponse
            {
                events = events,
                current_page = page,
                pages = (int)pageCount
            };
            return Ok(response);
        }

        [HttpPost("event/create")]
        [SwaggerOperation(Summary = "creates an event")]
        public async Task<ActionResult<Event>> RegisterEvent(Event item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEvent = await _eventRepo.AddEvent(item);

            if (newEvent == null)
            {
                return NotFound($"Event creator with Id: {item.event_creator_id} not found");
            }

            return Created("", new { data = newEvent, message = "Event created Successfully " });
        }

        [HttpGet("event/{id}/{user_id}")]
        [SwaggerOperation(Summary = "returns an event and a user")]
        public async Task<ActionResult<SingleEventResponse>> GetEventById(Guid id, int user_id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest($"Event with Id: {id} is not valid");
            }
            var single_event = await _eventRepo.GetEventById(id);

            if (single_event == null)
            {
                return NotFound($"Event with Id: {id} not found");
            }

            var user = await _user.GetUser(user_id);

            if( user == null)
            {
                return NotFound($"User with Id: {user_id} not found");
            }

            var response = new SingleEventResponse
            {
                event_activity = single_event,
                user = user
            };

            return Ok(response);
        }


        [HttpPatch("event/update/{id}")]
        [SwaggerOperation(Summary = "returns a updated event", Description = "This action updates an event")]
        public async Task<ActionResult<Event>> EditEvent(Event _event, Guid id)
        {
           
            var updatedEvent = await _eventRepo.UpdateEvent(_event,id);

            if (updatedEvent == null)
            {
                return NotFound($"Cannot find event with Id: {id}");
            }

            return Ok(new { data = updatedEvent, message = "Event updated sucessfully" });
        }


        [HttpDelete("event/delete/{id}")]
        [SwaggerOperation(Summary = "returns a deleted event.", Description = "This actions deletes an event")]
        public async Task<ActionResult<Event>> DeleteEvent(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Event with Id: {id} is not valid");
            }

            var deletedEvent = await _eventRepo.RemoveEventById(id).ConfigureAwait(false);

            if (deletedEvent == null)
            {
                return NotFound($"Cannot find event with Id: {id}");
            }

            return Ok(new { data = deletedEvent, message = "Event deleted successfully" });
        }

        #region creators
        [HttpPost("event/creator/create")]
        [SwaggerOperation(Summary = "creates a creator or user who can post an event.")]
        public async Task<ActionResult<Event>> CreateEventCreator(EventCreator creator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEvent = await _eventRepo.AddEventCreator(creator);

            return Created("", new { data = newEvent, meesage = "Event creator successfully created" });
        }

        [HttpGet("events/creator/{id}")]
        [SwaggerOperation(Summary = "returns list of events of particular creator or user")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserEvents(Guid id)
        {
            // checks if creator/user exists 
            // fyi: event creator can be also a user.
            var item = await _eventRepo.GetEventCreatorById(id);

            if (item is null)
            {
                return NotFound($"Event Creator with Id: {id} not found");
            }
            var events = await _eventRepo.GetCreatorEvents(id);

            return Ok(events);
        }

        [HttpGet("events/creators")]
        [SwaggerOperation(Summary = "returns a all creators of events")]
        public async Task<ActionResult<IEnumerable<EventCreator>>> GetEventCreators()
        {
            var creators = await _eventRepo.GetAllEventCreators();
            return Ok(creators);
        }

        [HttpGet("event/creator/{id}")]
        [SwaggerOperation(Summary = "returns a creator of an event")]
        public async Task<ActionResult<EventCreator>> GetEventCreatorById(Guid id)
        {
            var item = await _eventRepo.GetEventCreatorById(id);

            if (item is null)
            {
                return NotFound($"Event Creator with Id: {id} not found");
            } 

            return Ok( new { data = item });
        }
    }  
}
#endregion
#endregion