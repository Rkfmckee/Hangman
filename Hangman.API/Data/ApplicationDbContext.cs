using Hangman.API.Models;
using Hangman.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hangman.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<Game> Game { get; set; }
        public DbSet<Guess> Guess { get; set; }
        public DbSet<Words> Words { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        #region Override methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>().HasMany(ga => ga.Guesses).WithOne(gu => gu.Game).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Game>().Navigation(ga => ga.Guesses).AutoInclude();

            builder.Entity<Game>().HasOne(ga => ga.ChosenWord).WithMany(w => w.GamesUsed);
            builder.Entity<Game>().Navigation(ga => ga.ChosenWord).AutoInclude();

            builder.Entity<Game>().Property(ga => ga.GameStatus).HasConversion(new EnumToStringConverter<GameStatus>());
        }

        #endregion
    }
}
