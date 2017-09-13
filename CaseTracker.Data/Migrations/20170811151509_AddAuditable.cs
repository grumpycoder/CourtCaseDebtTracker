using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseTracker.Data.Migrations
{
    public partial class AddAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Comments",
                type: "DateTime2",
                nullable: true,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUser",
                table: "Comments",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "Comments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Comments",
                type: "DateTime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldNullable: true,
                oldDefaultValueSql: "GetDate()");
        }
    }
}
