using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiabeticSystem.Web.Migrations
{
    public partial class CompleteDBIII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Sensibilities",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Sensibilities",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "Records",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Ratios",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Ratios",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "Patients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Parameters",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Parameters",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Hour",
                table: "Checks",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Parameters");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Sensibilities",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Sensibilities",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Ratios",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Ratios",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hour",
                table: "Checks",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
