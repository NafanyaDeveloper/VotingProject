using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.CORE.EF
{
    public class VTBHackatonContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public VTBHackatonContext(DbContextOptions<VTBHackatonContext> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoom>()
                .HasKey(x => new { x.RoomId, x.UserId });

            modelBuilder.Entity<UserVariant>()
                .HasKey(x => new { x.VariantId, x.UserId });

            modelBuilder.Entity<RefreshToken>()
                .HasKey(rt => new { rt.UserId, rt.Token });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<UserRoom> UserRoom { get; set; }

        public DbSet<Poll> Polls { get; set; }

        public DbSet<Variant> Variants { get; set; }

        public DbSet<UserVariant> UserVariant { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
