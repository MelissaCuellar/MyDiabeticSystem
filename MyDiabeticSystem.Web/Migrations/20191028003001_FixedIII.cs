using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiabeticSystem.Web.Migrations
{
    public partial class FixedIII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "AspNetUsers");
        }
    }
}
