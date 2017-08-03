using Microsoft.EntityFrameworkCore;
using CaseTracker.Core.Models;
using System.Linq;

namespace CaseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Filing> Filings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Jurisdiction> Jurisdictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own confguration here

            foreach (var p in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null))
            {
                p.SetMaxLength(500);
            }

            modelBuilder.Entity<Filing>()
                .Property(t => t.CreateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Filing>()
                .Property(t => t.UpdateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Comment>()
                .Property(t => t.CreateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            // modelBuilder.Entity<Comment>()
            //     .HasOne(p => p.Filing)
            //     .WithMany(b => b.Comments);
        }
    }
}