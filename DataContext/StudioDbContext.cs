using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioaMvc.Models;

namespace StudioaMvc.DataContext
{
    public class StudioDbContext :IdentityDbContext<AppUser>
    {
        public StudioDbContext(DbContextOptions<StudioDbContext> opt):base(opt)
        {

        }
       public DbSet<OurTeam> OurTeams { get; set; }
    }
}
