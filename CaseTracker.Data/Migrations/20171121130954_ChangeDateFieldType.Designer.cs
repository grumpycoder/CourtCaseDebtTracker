using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CaseTracker.Data;

namespace CaseTracker.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20171121130954_ChangeDateFieldType")]
    partial class ChangeDateFieldType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CaseTracker.Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("CourtId");

                    b.Property<DateTime?>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int?>("Date");

                    b.Property<int>("FilingId");

                    b.Property<string>("Judge")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Summary")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CaseId");

                    b.Property<DateTime?>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("FilingId");

                    b.Property<string>("Text")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Court", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("JurisdictionId");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("JurisdictionId");

                    b.ToTable("Courts");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.FilingTag", b =>
                {
                    b.Property<int>("FilingId");

                    b.Property<int>("TagId");

                    b.HasKey("FilingId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("FilingTags");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Jurisdiction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Jurisdictions");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Litigant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FilingId");

                    b.Property<int>("LitigantType");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Litigants");

                    b.HasDiscriminator<int>("LitigantType");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("RoleId")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("Value")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Defendant", b =>
                {
                    b.HasBaseType("CaseTracker.Core.Models.Litigant");

                    b.Property<int?>("CaseId");

                    b.HasIndex("CaseId");

                    b.ToTable("Defendant");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Plaintiff", b =>
                {
                    b.HasBaseType("CaseTracker.Core.Models.Litigant");

                    b.Property<int?>("CaseId");

                    b.HasIndex("CaseId");

                    b.ToTable("Plaintiff");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Case", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Court", "Court")
                        .WithMany("Filings")
                        .HasForeignKey("CourtId");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Comment", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Case")
                        .WithMany("Comments")
                        .HasForeignKey("CaseId");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Court", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Jurisdiction", "Jurisdiction")
                        .WithMany()
                        .HasForeignKey("JurisdictionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.FilingTag", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Case", "Case")
                        .WithMany("Tags")
                        .HasForeignKey("FilingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CaseTracker.Core.Models.Tag", "Tag")
                        .WithMany("Filings")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CaseTracker.Core.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Defendant", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Case")
                        .WithMany("Defendants")
                        .HasForeignKey("CaseId");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Plaintiff", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Case")
                        .WithMany("Plaintiffs")
                        .HasForeignKey("CaseId");
                });
        }
    }
}
