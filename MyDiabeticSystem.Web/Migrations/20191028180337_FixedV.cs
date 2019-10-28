using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDiabeticSystem.Web.Migrations
{
    public partial class FixedV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);
        }
    }
}
