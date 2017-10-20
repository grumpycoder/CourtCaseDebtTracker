using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseTracker.Data.Migrations
{
    public partial class PopulateJurisdictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT Jurisdictions ON");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (1, 'F', 'Federal Appellate')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (2, 'FS', 'Federal Special')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (3, 'FBP', 'Federal Bankruptcy Panel')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (4, 'FB', 'Federal Bankruptcy')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (5, 'FD', 'Federal District')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (6, 'S', 'State Supreme')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (7, 'SA', 'State Appellate')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (8, 'SS', 'State Special')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (9, 'SAG', 'State Attorney General')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (10, 'ST', 'State Trial')");
            migrationBuilder.Sql("INSERT INTO Jurisdictions (Id, Abbreviation, Name) VALUES (11, 'C', 'Committee')");
            migrationBuilder.Sql("SET IDENTITY_INSERT Jurisdictions OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Jurisdictions WHERE Id <= 11");
        }
    }
}
