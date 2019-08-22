using LanguageHelper.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageHelper.DataAccessLayer
{
    public class LanguageHelperDbContext: DbContext
    {
        public LanguageHelperDbContext(DbContextOptions<LanguageHelperDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasMany(u => u.Sheets).WithOne(sh=>sh.User).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
    }
}
