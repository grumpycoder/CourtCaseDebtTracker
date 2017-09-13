using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseTracker.Data.Migrations
{
    public partial class Litigants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Litigants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilingId = table.Column<int>(nullable: false),
                    LitigantType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
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

            migrationBuilder.Sql("INSERT INTO Litigants (LitigantType, FilingId, Name) SELECT 1 as Discriminator, Id, Defendant FROM Filings ");
            migrationBuilder.Sql("INSERT INTO Litigants (LitigantType, FilingId, Name) SELECT 2 as Discriminator, Id, Plaintiff FROM Filings ");

            migrationBuilder.CreateIndex(
                name: "IX_Litigants_FilingId",
                table: "Litigants",
                column: "FilingId");

            migrationBuilder.DropColumn(
              name: "Defendant",
              table: "Filings");

            migrationBuilder.DropColumn(
                name: "Plaintiff",
                table: "Filings");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Litigants");

            migrationBuilder.AddColumn<string>(
                name: "Defendant",
                table: "Filings",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plaintiff",
                table: "Filings",
                maxLength: 500,
                nullable: true);
        }
    }
}
