using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiabeticSystem.Web.Migrations
{
    public partial class Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "Patients");

            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirth",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FathersEmail",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FathersEmail",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "Patients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBirth",
                table: "Patients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
