using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Models; // <-- To pozwala widzieæ modele Survey, Option i Vote

namespace SurveySystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        // TE TRZY LINIJKI ROZWI¥¯¥ CZERWONE B£ÊDY:
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vote>()
                .HasIndex(v => new { v.UserId, v.SurveyId })
                .IsUnique();

            builder.Entity<Vote>()
                .HasOne(v => v.Survey)
                .WithMany(s => s.Votes)
                .HasForeignKey(v => v.SurveyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}