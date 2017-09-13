using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CaseTracker.Data;

namespace CaseTracker.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170804193644_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CaseTracker.Core.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(500);

                    b.Property<int>("FilingId");

                    b.Property<string>("Text")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("FilingId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Court", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(500);

                    b.Property<int>("JurisdictionId");

                    b.Property<string>("Name")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("JurisdictionId");

                    b.ToTable("Courts");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Filing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CaseNumber")
                        .HasMaxLength(500);

                    b.Property<int>("CourtId");

                    b.Property<DateTime?>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("DateFiled");

                    b.Property<string>("Defendant")
                        .HasMaxLength(500);

                    b.Property<int>("FilingId");

                    b.Property<string>("Judge")
                        .HasMaxLength(500);

                    b.Property<string>("Plaintiff")
                        .HasMaxLength(500);

                    b.Property<string>("Summary")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime2")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<string>("UpdatedUser")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CourtId");

                    b.ToTable("Filings");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Jurisdiction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Jurisdictions");
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Comment", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Filing")
                        .WithMany("Comments")
                        .HasForeignKey("FilingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Court", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Jurisdiction", "Jurisdiction")
                        .WithMany()
                        .HasForeignKey("JurisdictionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CaseTracker.Core.Models.Filing", b =>
                {
                    b.HasOne("CaseTracker.Core.Models.Court", "Court")
                        .WithMany()
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
