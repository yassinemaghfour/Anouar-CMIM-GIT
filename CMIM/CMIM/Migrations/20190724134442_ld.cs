using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class ld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EnfantId",
                table: "Dossiers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_EnfantId",
                table: "Dossiers",
                column: "EnfantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dossiers_Enfant_EnfantId",
                table: "Dossiers",
                column: "EnfantId",
                principalTable: "Enfant",
                principalColumn: "EnfantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dossiers_Enfant_EnfantId",
                table: "Dossiers");

            migrationBuilder.DropIndex(
                name: "IX_Dossiers_EnfantId",
                table: "Dossiers");

            migrationBuilder.DropColumn(
                name: "EnfantId",
                table: "Dossiers");
        }
    }
}
