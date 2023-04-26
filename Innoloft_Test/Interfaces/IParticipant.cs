using System.Numerics;

namespace Innoloft_Test.Interfaces
{
    public interface IParticipant
    {
        Task ApproveInvite(int event_id, int participant_id);
    }
}
