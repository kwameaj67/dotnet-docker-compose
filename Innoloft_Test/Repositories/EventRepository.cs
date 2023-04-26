using Innoloft_Test.Data;
using Innoloft_Test.Dtos;
using Innoloft_Test.Interfaces;
using Innoloft_Test.Models;
using Microsoft.EntityFrameworkCore;
using static Innoloft_Test.Models.Event;

namespace Innoloft_Test.Repositories
{
    public class EventRepository : IEvent
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event?> AddEvent(Event _event)
        {
            // check if creator id exists
            var creator = await _context.event_creators.FindAsync(_event.event_creator_id);
            if (creator is null)
            {
                return null;
            }
            var newEvent = new Event()
            {
                id = Guid.NewGuid(),
                title = _event.title,
                description = _event.description,
                event_creator_id = _event.event_creator_id,
                event_type = _event.event_type,
                location = _event.location,
                created_at = DateTime.Now,
                start_date = _event.start_date,
                end_date = _event.end_date,
                updated_at = _event.updated_at
            };
            await _context.events.AddAsync(newEvent);
            await _context.SaveChangesAsync();

            return newEvent;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var events = await _context.events.AsNoTracking().ToListAsync();
            return events;
        }

        public async Task<EventCreator> AddEventCreator(EventCreator creator)
        {
            var newCreator = new EventCreator()
            {
                id = Guid.NewGuid(),
                name = creator.name
            };

            await _context.event_creators.AddAsync(newCreator);
            await _context.SaveChangesAsync();
            return creator;
        }
       

        public async Task<IEnumerable<EventCreator>> GetAllEventCreators()
        {
            var creators = await _context.event_creators.AsNoTracking().ToListAsync();
            return creators;
        }

        public async Task<Event?> GetEventById(Guid id)
        {
            var event_item = await _context.events.FindAsync(id);
            if (event_item == null)
            {
                return null;
            }
            return event_item;
        }

        public Task InviteParticipant(Guid event_id)
        {
            throw new NotImplementedException();
        }

        public async Task<Event?> RemoveEventById(Guid id)
        {
            var item = await _context.events.FindAsync(id);
            if(item == null)
            {
                return null;
            }

            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
            return item;
        }

        public async Task<Event?> UpdateEvent(Event _event, Guid id)
        {
            var item = await _context.events.FindAsync(id);
            if (item == null)
            {
                return null;
            }
            item.title = _event.title;
            item.description = string.IsNullOrEmpty(_event.description) ? item.description : _event.description;
            item.location = string.IsNullOrEmpty(_event.location) ? item.location : _event.location;
            item.event_type = string.IsNullOrEmpty(_event.event_type) ? item.event_type : _event.event_type;
            item.start_date = _event.start_date.HasValue ? _event.start_date : item.start_date;
            item.end_date = _event.end_date.HasValue ? _event.end_date : item.end_date;
            item.updated_at = DateTime.Now;

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<Event>> GetCreatorEvents(Guid event_creator_id)
        {
            var events = await _context.events.Where(v => v.event_creator_id == event_creator_id).ToListAsync();
            return events;
        }

        public async Task<EventCreator> GetEventCreatorById(Guid id)
        {
            var creator = await _context.event_creators.FindAsync(id);

            if(creator == null)
            {
                return null;
            }
            return creator;
        }
    }
}
