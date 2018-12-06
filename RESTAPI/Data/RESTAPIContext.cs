using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;

namespace RESTAPI.Data
{
    // ReSharper disable once InconsistentNaming
    public class RESTAPIContext : DbContext
    {
        public RESTAPIContext (DbContextOptions<RESTAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Store { get; set; }

        public DbSet<Event> Event { get; set; }

        public DbSet<Promotion> Promotion { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().Ignore(s => s.Establishments);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<UserEstablishment>()
                .HasKey(ue => new {ue.UserId, ue.EstablishmentId});
            modelBuilder.Entity<UserEstablishment>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.Subscriptions);
            modelBuilder.Entity<UserEstablishment>()
                .HasOne(ue => ue.Establishment)
                .WithMany(e => e.SubscribedUsers);
        }

        public DbSet<Establishment> Establishment { get; set; }
    }
}
