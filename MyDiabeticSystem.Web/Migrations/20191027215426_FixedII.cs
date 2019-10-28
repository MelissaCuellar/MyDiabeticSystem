using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiabeticSystem.Web.Migrations
{
    public partial class FixedII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CanEdit",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CanEdit",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
