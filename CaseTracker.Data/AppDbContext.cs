using CaseTracker.Core;
using CaseTracker.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaseTracker.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IUserService _userService;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserService userService)
            : base(options)
        {
            _userService = userService;
        }

        public DbSet<Case> Cases { get; set; }

        public DbSet<Litigant> Litigants { get; set; }
        public DbSet<Defendant> Defendants { get; set; }
        public DbSet<Plaintiff> Plaintiffs { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Jurisdiction> Jurisdictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add your own confguration here
            base.OnModelCreating(modelBuilder);

            foreach (var p in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                p.IsUnicode(false);
            }

            foreach (var p in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null))
            {
                p.SetMaxLength(500);
            }

            modelBuilder.Entity<Case>()
                        .HasOne(p => p.Court)
                        .WithMany(b => b.Filings)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Case>()
                .Property(t => t.CreateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Case>()
                .Property(t => t.UpdateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Comment>()
                .Property(t => t.CreateDate)
                .HasColumnType("DateTime2")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Tag>().Property(t => t.Name).HasMaxLength(50);

            modelBuilder.Entity<FilingTag>().ToTable("FilingTags")
                .HasKey(x => new { x.FilingId, x.TagId });

            modelBuilder.Entity<FilingTag>().HasOne(pt => pt.Case).WithMany(p => p.Tags).HasForeignKey(pt => pt.FilingId);

            modelBuilder.Entity<FilingTag>().HasOne(pt => pt.Tag).WithMany(p => p.Filings).HasForeignKey(pt => pt.TagId);

            modelBuilder.Entity<Litigant>()
               .HasDiscriminator<int>("LitigantType")
               .HasValue<Defendant>(1)
               .HasValue<Plaintiff>(2);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || e.State == EntityState.Modified))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
                AddTimestamps();
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || e.State == EntityState.Modified))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }
            AddTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = _userService.CurrentUser;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((AuditableEntity)entity.Entity).CreateDate = DateTime.UtcNow;
                    ((AuditableEntity)entity.Entity).CreatedUser = currentUsername;
                }

                ((AuditableEntity)entity.Entity).UpdateDate = DateTime.UtcNow;
                ((AuditableEntity)entity.Entity).UpdatedUser = currentUsername;
            }
        }

    }


    public class TemporaryDbContextFactory : IDbContextFactory<AppDbContext>
    {
        public AppDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=CaseTracker;Integrated Security=True");
            return new AppDbContext(builder.Options);
        }
    }

}