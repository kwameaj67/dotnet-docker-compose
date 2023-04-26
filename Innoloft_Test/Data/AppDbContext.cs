using Microsoft.EntityFrameworkCore;
using Innoloft_Test.Models;
using Microsoft.Extensions.Options;
using Innoloft_Test.Dtos;

namespace Innoloft_Test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(){
        
        }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Event> events { get; set; } = null!;

        public DbSet<EventCreator> event_creators { get; set; } 

        public DbSet<Participant> users { get; set; } 

        public DbSet<EventInvitation> event_invitation { get; set; }

        public DbSet<EventParticipation> event_participation { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var connectionString = "server=localhost;port=3306;user=root;password=my-server-pw;database=innoloft";
        //        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //    }
        //}
    }


}
