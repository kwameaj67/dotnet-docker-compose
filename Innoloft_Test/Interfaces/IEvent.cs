using System;
using System.Numerics;
using Innoloft_Test.Dtos;
using Innoloft_Test.Models;

namespace Innoloft_Test.Interfaces
{
    public interface IEvent
    {
        Task<Event?> AddEvent(Event _event);

        Task<EventCreator> AddEventCreator(EventCreator creator);

        Task<EventCreator> GetEventCreatorById(Guid id);

        Task<IEnumerable<Event>> GetAllEvents();

        Task<IEnumerable<EventCreator>> GetAllEventCreators();

        Task<Event?> GetEventById(Guid id);

        Task<Event?> RemoveEventById(Guid id);

        Task<Event?> UpdateEvent(Event _event, Guid id);

        Task<IEnumerable<EventParticipation>> GetAllEventParticipation();

        Task<EventParticipation> AttendEvent(Guid event_id, Guid user_id);

        Task InviteParticipant(Guid event_id);

        Task<IEnumerable<Event>> GetCreatorEvents(Guid event_creator_id);

    }
}

