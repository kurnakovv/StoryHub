using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoryHub.BL.Models;

namespace StoryHub.BL.Db_Context
{
    public class AppDbContext : IdentityDbContext<Storyteller>//: DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // You can remove this method and use connection 
        // strings from appsettings.json by removing the empty constructor.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-N9EORNQ;Database=StoryHub;Trusted_Connection=True;");
        }

        public DbSet<Storyteller> Storytellers { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryCategory> StoryCategories { get; set; }
    }
}
