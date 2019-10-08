using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMIM.Migrations
{
    public partial class ldldld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityPlace",
                columns: table => new
                {
                    placdeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityPlace", x => x.placdeId);
                });

            migrationBuilder.CreateTable(
                name: "Bordereau",
                columns: table => new
                {
                    BordereauId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordereau", x => x.BordereauId);
                });

            migrationBuilder.CreateTable(
                name: "bu",
                columns: table => new
                {
                    BuId = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bu", x => x.BuId);
                });

            migrationBuilder.CreateTable(
                name: "Regularisation",
                columns: table => new
                {
                    RegularisationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateRegularisation = table.Column<DateTime>(nullable: false),
                    Mois = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regularisation", x => x.RegularisationID);
                });

            migrationBuilder.CreateTable(
                name: "Remboursser",
                columns: table => new
                {
                    rembourssementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateRembourssement = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remboursser", x => x.rembourssementId);
                });

            migrationBuilder.CreateTable(
                name: "sites",
                columns: table => new
                {
                    SiteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sites", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    CompanyId1 = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Etat = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    matricule = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    adresse = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    matriculecmim = table.Column<string>(nullable: true),
                    PlaceplacdeId = table.Column<long>(nullable: false),
                    buId = table.Column<string>(nullable: true),
                    company = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.matricule);
                    table.ForeignKey(
                        name: "FK_employees_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employees_ActivityPlace_PlaceplacdeId",
                        column: x => x.PlaceplacdeId,
                        principalTable: "ActivityPlace",
                        principalColumn: "placdeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employees_sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employees_bu_buId",
                        column: x => x.buId,
                        principalTable: "bu",
                        principalColumn: "BuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conjoint",
                columns: table => new
                {
                    ConjointId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Employeematricule = table.Column<string>(nullable: true),
                    matriculecmim = table.Column<string>(nullable: true),
                    DateNs = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conjoint", x => x.ConjointId);
                    table.ForeignKey(
                        name: "FK_Conjoint_employees_Employeematricule",
                        column: x => x.Employeematricule,
                        principalTable: "employees",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regul_Emp",
                columns: table => new
                {
                    regul_empID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RégularisationRegularisationID = table.Column<long>(nullable: true),
                    EmployeeMatricule = table.Column<string>(nullable: true),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regul_Emp", x => x.regul_empID);
                    table.ForeignKey(
                        name: "FK_Regul_Emp_employees_EmployeeMatricule",
                        column: x => x.EmployeeMatricule,
                        principalTable: "employees",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regul_Emp_Regularisation_RégularisationRegularisationID",
                        column: x => x.RégularisationRegularisationID,
                        principalTable: "Regularisation",
                        principalColumn: "RegularisationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    referance = table.Column<string>(nullable: false),
                    employeematricule = table.Column<string>(nullable: true),
                    ConjointId = table.Column<long>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    dateDossier = table.Column<DateTime>(nullable: false),
                    dateLunette = table.Column<DateTime>(nullable: true),
                    montant = table.Column<double>(nullable: false),
                    Mt_radio = table.Column<double>(nullable: false),
                    Mt_analyse = table.Column<double>(nullable: false),
                    MtSoins_D = table.Column<double>(nullable: false),
                    MtProthese_D = table.Column<double>(nullable: false),
                    Devis_D = table.Column<double>(nullable: false),
                    MtVisite_M = table.Column<double>(nullable: false),
                    MtPharmacie_M = table.Column<double>(nullable: false),
                    MtVisite_L = table.Column<double>(nullable: false),
                    MtCadre_L = table.Column<double>(nullable: false),
                    MtGi_L = table.Column<double>(nullable: false),
                    avance = table.Column<double>(nullable: false),
                    rembourse = table.Column<double>(nullable: false),
                    diff = table.Column<double>(nullable: false),
                    rembourssementId = table.Column<long>(nullable: true),
                    BordereauId = table.Column<long>(nullable: true),
                    etat = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.referance);
                    table.ForeignKey(
                        name: "FK_Dossiers_Bordereau_BordereauId",
                        column: x => x.BordereauId,
                        principalTable: "Bordereau",
                        principalColumn: "BordereauId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossiers_Conjoint_ConjointId",
                        column: x => x.ConjointId,
                        principalTable: "Conjoint",
                        principalColumn: "ConjointId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossiers_employees_employeematricule",
                        column: x => x.employeematricule,
                        principalTable: "employees",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossiers_Remboursser_rembourssementId",
                        column: x => x.rembourssementId,
                        principalTable: "Remboursser",
                        principalColumn: "rembourssementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enfant",
                columns: table => new
                {
                    EnfantId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    DateNs = table.Column<DateTime>(nullable: false),
                    DateVs = table.Column<DateTime>(nullable: false),
                    ConjointId = table.Column<long>(nullable: false),
                    employeematricule = table.Column<string>(nullable: true),
                    MatriculeCmim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfant", x => x.EnfantId);
                    table.ForeignKey(
                        name: "FK_Enfant_Conjoint_ConjointId",
                        column: x => x.ConjointId,
                        principalTable: "Conjoint",
                        principalColumn: "ConjointId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enfant_employees_employeematricule",
                        column: x => x.employeematricule,
                        principalTable: "employees",
                        principalColumn: "matricule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "list",
                columns: table => new
                {
                    listId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RembouserembourssementId = table.Column<long>(nullable: false),
                    Dossierreferance = table.Column<string>(nullable: true),
                    montant = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_list", x => x.listId);
                    table.ForeignKey(
                        name: "FK_list_Dossiers_Dossierreferance",
                        column: x => x.Dossierreferance,
                        principalTable: "Dossiers",
                        principalColumn: "referance",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_list_Remboursser_RembouserembourssementId",
                        column: x => x.RembouserembourssementId,
                        principalTable: "Remboursser",
                        principalColumn: "rembourssementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyId1",
                table: "Company",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Company_SiteId",
                table: "Company",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Conjoint_Employeematricule",
                table: "Conjoint",
                column: "Employeematricule");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_BordereauId",
                table: "Dossiers",
                column: "BordereauId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_ConjointId",
                table: "Dossiers",
                column: "ConjointId");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_employeematricule",
                table: "Dossiers",
                column: "employeematricule");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_rembourssementId",
                table: "Dossiers",
                column: "rembourssementId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_CompanyId",
                table: "employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_PlaceplacdeId",
                table: "employees",
                column: "PlaceplacdeId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_SiteId",
                table: "employees",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_buId",
                table: "employees",
                column: "buId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfant_ConjointId",
                table: "Enfant",
                column: "ConjointId");

            migrationBuilder.CreateIndex(
                name: "IX_Enfant_employeematricule",
                table: "Enfant",
                column: "employeematricule");

            migrationBuilder.CreateIndex(
                name: "IX_list_Dossierreferance",
                table: "list",
                column: "Dossierreferance",
                unique: true,
                filter: "[Dossierreferance] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_list_RembouserembourssementId",
                table: "list",
                column: "RembouserembourssementId");

            migrationBuilder.CreateIndex(
                name: "IX_Regul_Emp_EmployeeMatricule",
                table: "Regul_Emp",
                column: "EmployeeMatricule");

            migrationBuilder.CreateIndex(
                name: "IX_Regul_Emp_RégularisationRegularisationID",
                table: "Regul_Emp",
                column: "RégularisationRegularisationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SiteId",
                table: "Users",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enfant");

            migrationBuilder.DropTable(
                name: "list");

            migrationBuilder.DropTable(
                name: "Regul_Emp");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "Regularisation");

            migrationBuilder.DropTable(
                name: "Bordereau");

            migrationBuilder.DropTable(
                name: "Conjoint");

            migrationBuilder.DropTable(
                name: "Remboursser");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ActivityPlace");

            migrationBuilder.DropTable(
                name: "bu");

            migrationBuilder.DropTable(
                name: "sites");
        }
    }
}
