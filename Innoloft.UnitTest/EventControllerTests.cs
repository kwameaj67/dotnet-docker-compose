using Xunit;
using Moq;
using Innoloft_Test.Interfaces;
using Innoloft_Test.Models;
using Innoloft_Test.Controllers;
using Innoloft_Test.Data;

namespace Innoloft.UnitTest
{
    public class EventControllerTests
    {
        private readonly IEvent _event;
        private readonly IUser _user;
        private readonly AppDbContext _dbcontext;

        public EventControllerTests()
        {
            var events = new Mock<IEvent>();

            events.Setup(repo => repo.GetAllEventCreators())
                .ReturnsAsync(GetAllCreators());

            _event = events.Object;
        }

        [Fact]
        public async Task TestGetAllEventCreators()
        {
            // arrange 
            var controller = new EventsController(_event, _user, _dbcontext);

            //act
            var result = await controller.GetEventCreators();

            //assert
            Assert.NotNull(result);
        }

        // private methods
        public IEnumerable<EventCreator> GetAllCreators()
        {
            var creators = new List<EventCreator>();

            creators.Add(new EventCreator
            {
                id= Guid.NewGuid(),
                name = "Kwame Boateng"
            }); 
            creators.Add(new EventCreator
            {
                id = Guid.NewGuid(),
                name = "Innoloft Admin"
            });
            creators.Add(new EventCreator
            {
                id = Guid.NewGuid(),
                name = "George Andrew"
            });
            return creators;
        }
    }
}