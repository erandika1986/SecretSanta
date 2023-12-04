using Microsoft.EntityFrameworkCore;
using SecretSanta.API.Data.Configurations;
using SecretSanta.API.Models;
using System.Diagnostics;
using System.Net;

namespace SecretSanta.API.Data
{
    public class SecretSantaContext : DbContext
    {
        public SecretSantaContext(DbContextOptions<SecretSantaContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message))
           .EnableSensitiveDataLogging();
            optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new AssignedBuddyConfiguration());
        }

        public DbSet<Event>  Events => Set<Event>();
        public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();
        public DbSet<AssignedBuddy> AssignedBuddies => Set<AssignedBuddy>();
    }
}
