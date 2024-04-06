using Microsoft.EntityFrameworkCore;
using DatabaseEntityLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.ComponentModel;

namespace DataBaseContext
{
    public class DB_Context_Class : DbContext
    {
        public DbSet<Predmeti> Predmeti { get; set; }
        public DbSet<Oblasti> Oblasti { get; set; }
        public DbSet<Zadaci> Zadaci { get; set; }

        public DB_Context_Class(DbContextOptions<DB_Context_Class> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Predmeti>()
                .Property(i => i.Name)
            .IsRequired();
            modelBuilder.Entity<Oblasti>()
                .Property(i => i.Name)
            .IsRequired();
            modelBuilder.Entity<Zadaci>()
                .Property(p => p.IDPredmet)
                .IsRequired();
            modelBuilder.Entity<Zadaci>()
                .Property(p => p.IDOblast)
                .IsRequired();
            modelBuilder.Entity<Zadaci>()
                .Property(p => p.Pitanje)
                .IsRequired();
            modelBuilder.Entity<Zadaci>()
                .Property(p => p.Nivo)
                .IsRequired();
        }
    }
}
