using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class add_User_To_Dossier_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Dossiers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_UserId",
                table: "Dossiers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dossiers_Users_UserId",
                table: "Dossiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dossiers_Users_UserId",
                table: "Dossiers");

            migrationBuilder.DropIndex(
                name: "IX_Dossiers_UserId",
                table: "Dossiers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dossiers");
        }
    }
}
