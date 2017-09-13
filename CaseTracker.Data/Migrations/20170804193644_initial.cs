using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CaseTracker.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Jurisdictions",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //         Abbreviation = table.Column<string>(maxLength: 500, nullable: true),
            //         Name = table.Column<string>(maxLength: 500, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Jurisdictions", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Courts",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //         Abbreviation = table.Column<string>(maxLength: 500, nullable: true),
            //         JurisdictionId = table.Column<int>(nullable: false),
            //         Name = table.Column<string>(maxLength: 500, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Courts", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Courts_Jurisdictions_JurisdictionId",
            //             column: x => x.JurisdictionId,
            //             principalTable: "Jurisdictions",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Filings",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //         CaseNumber = table.Column<string>(maxLength: 500, nullable: true),
            //         CourtId = table.Column<int>(nullable: false),
            //         CreateDate = table.Column<DateTime>(type: "DateTime2", nullable: true, defaultValueSql: "GetDate()"),
            //         CreatedUser = table.Column<string>(maxLength: 500, nullable: true),
            //         DateFiled = table.Column<DateTime>(nullable: true),
            //         Defendant = table.Column<string>(maxLength: 500, nullable: true),
            //         FilingId = table.Column<int>(nullable: false),
            //         Judge = table.Column<string>(maxLength: 500, nullable: true),
            //         Plaintiff = table.Column<string>(maxLength: 500, nullable: true),
            //         Summary = table.Column<string>(maxLength: 500, nullable: true),
            //         UpdateDate = table.Column<DateTime>(type: "DateTime2", nullable: true, defaultValueSql: "GetDate()"),
            //         UpdatedUser = table.Column<string>(maxLength: 500, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Filings", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Filings_Courts_CourtId",
            //             column: x => x.CourtId,
            //             principalTable: "Courts",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Comments",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(nullable: false)
            //             .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //         CreateDate = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValueSql: "GetDate()"),
            //         CreatedUser = table.Column<string>(maxLength: 500, nullable: true),
            //         FilingId = table.Column<int>(nullable: false),
            //         Text = table.Column<string>(maxLength: 500, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Comments", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_Comments_Filings_FilingId",
            //             column: x => x.FilingId,
            //             principalTable: "Filings",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Comments_FilingId",
            //     table: "Comments",
            //     column: "FilingId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Courts_JurisdictionId",
            //     table: "Courts",
            //     column: "JurisdictionId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_Filings_CourtId",
            //     table: "Filings",
            //     column: "CourtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Filings");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Jurisdictions");
        }
    }
}
