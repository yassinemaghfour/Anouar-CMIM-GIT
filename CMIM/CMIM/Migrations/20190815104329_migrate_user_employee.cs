using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class migrate_user_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Site",
                table: "employees");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_employees_UserId",
                table: "employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Users_UserId",
                table: "employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Users_UserId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_UserId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "employees");

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "employees",
                nullable: true);
        }
    }
}
