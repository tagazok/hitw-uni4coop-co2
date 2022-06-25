using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using api.Data.Models;

namespace api.Data
{
    public partial class HITWDbContext : DbContext
    {
        public HITWDbContext()
        {
        }

        public HITWDbContext(DbContextOptions<HITWDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK__Histories__TripI__6E01572D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Histories_User");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Trips_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
