using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class delete_site_from_emp_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_Company_CompanyId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_sites_SiteId",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_sites_SiteId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_employees_CompanyId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_SiteId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "Users",
                newName: "siteId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SiteId",
                table: "Users",
                newName: "IX_Users_siteId");

            migrationBuilder.AlterColumn<long>(
                name: "siteId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_sites_siteId",
                table: "Users",
                column: "siteId",
                principalTable: "sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_sites_siteId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "siteId",
                table: "Users",
                newName: "SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_siteId",
                table: "Users",
                newName: "IX_Users_SiteId");

            migrationBuilder.AlterColumn<long>(
                name: "SiteId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "employees",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SiteId",
                table: "employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<string>(nullable: false),
                    CompanyId1 = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Company_Company_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_CompanyId",
                table: "employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_SiteId",
                table: "employees",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyId1",
                table: "Company",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Company_SiteId",
                table: "Company",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_Company_CompanyId",
                table: "employees",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_sites_SiteId",
                table: "employees",
                column: "SiteId",
                principalTable: "sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_sites_SiteId",
                table: "Users",
                column: "SiteId",
                principalTable: "sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
