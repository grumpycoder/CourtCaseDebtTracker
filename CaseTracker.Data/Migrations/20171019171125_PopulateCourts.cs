using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseTracker.Data.Migrations
{
    public partial class PopulateCourts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET Courts Jurisdictions ON");
            migrationBuilder.Sql("INSERT INTO Courts (Id, Abbreviation, Name) VALUES (1, 'F', 'Federal Appellate')");

            migrationBuilder.Sql("SET IDENTITY_INSERT Courts OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
