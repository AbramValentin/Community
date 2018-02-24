using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Community.Data.Tables;

namespace Community.Data
{
    class CommunityDbContext : IdentityDbContext<User>
    {
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingCategory> MeetingCategories { get; set; }
        

        public CommunityDbContext(DbContextOptions<CommunityDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserMeetings>()
                .HasKey(m => new { m.UserId, m.MeetingId });
            base.OnModelCreating(builder);
        }
    }
}
