using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseTracker.Data.Migrations
{
    public partial class DeleteCaseNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseNumber",
                table: "Cases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaseNumber",
                table: "Cases",
                unicode: false,
                maxLength: 500,
                nullable: true);
        }
    }
}
