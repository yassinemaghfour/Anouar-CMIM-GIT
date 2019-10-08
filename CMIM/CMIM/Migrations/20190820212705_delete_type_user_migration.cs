using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class delete_type_user_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                nullable: true);
        }
    }
}
