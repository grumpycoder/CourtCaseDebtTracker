using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CaseTracker.Data.Migrations
{
    public partial class CreateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jurisdictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Abbreviation = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    JurisdictionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courts_Jurisdictions_JurisdictionId",
                        column: x => x.JurisdictionId,
                        principalTable: "Jurisdictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    CaseNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    CourtId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DateTime2", nullable: true, defaultValueSql: "GetDate()"),
                    CreatedUser = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    DateFiled = table.Column<DateTime>(nullable: true),
                    FilingId = table.Column<int>(nullable: false),
                    Judge = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Summary = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "DateTime2", nullable: true, defaultValueSql: "GetDate()"),
                    UpdatedUser = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filings_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "DateTime2", nullable: true, defaultValueSql: "GetDate()"),
                    CreatedUser = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    FilingId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdatedUser = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Filings_FilingId",
                        column: x => x.FilingId,
                        principalTable: "Filings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilingTags",
                columns: table => new
                {
                    FilingId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilingTags", x => new { x.FilingId, x.TagId });
                    table.ForeignKey(
                        name: "FK_FilingTags_Filings_FilingId",
                        column: x => x.FilingId,
                        principalTable: "Filings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilingTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Litigants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilingId = table.Column<int>(nullable: false),
                    LitigantType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Litigants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Litigants_Filings_FilingId",
                        column: x => x.FilingId,
                        principalTable: "Filings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FilingId",
                table: "Comments",
                column: "FilingId");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_JurisdictionId",
                table: "Courts",
                column: "JurisdictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Filings_CourtId",
                table: "Filings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_FilingTags_TagId",
                table: "FilingTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Litigants_FilingId",
                table: "Litigants",
                column: "FilingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FilingTags");

            migrationBuilder.DropTable(
                name: "Litigants");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Filings");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Jurisdictions");
        }
    }
}
