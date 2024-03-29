﻿// <auto-generated />
using System;
using CMIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CMIM.Migrations
{
    [DbContext(typeof(CmimDBContext))]
    [Migration("20190820212705_delete_type_user_migration")]
    partial class delete_type_user_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMIM.Models.ActivityPlace", b =>
                {
                    b.Property<long>("placdeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("placdeId");

                    b.ToTable("ActivityPlace");
                });

            modelBuilder.Entity("CMIM.Models.Bordereau", b =>
                {
                    b.Property<long>("BordereauId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company");

                    b.Property<DateTime>("DateCreation");

                    b.HasKey("BordereauId");

                    b.ToTable("Bordereau");
                });

            modelBuilder.Entity("CMIM.Models.BU", b =>
                {
                    b.Property<string>("BuId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.HasKey("BuId");

                    b.ToTable("bu");
                });

            modelBuilder.Entity("CMIM.Models.Conjoint", b =>
                {
                    b.Property<long>("ConjointId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateNs");

                    b.Property<string>("Employeematricule");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("matriculecmim");

                    b.HasKey("ConjointId");

                    b.HasIndex("Employeematricule");

                    b.ToTable("Conjoint");
                });

            modelBuilder.Entity("CMIM.Models.Dossier", b =>
                {
                    b.Property<string>("referance")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BordereauId");

                    b.Property<long?>("ConjointId");

                    b.Property<double>("Devis_D");

                    b.Property<long?>("EnfantId");

                    b.Property<double>("MtCadre_L");

                    b.Property<double>("MtGi_L");

                    b.Property<double>("MtPharmacie_M");

                    b.Property<double>("MtProthese_D");

                    b.Property<double>("MtSoins_D");

                    b.Property<double>("MtVisite_L");

                    b.Property<double>("MtVisite_M");

                    b.Property<double>("Mt_analyse");

                    b.Property<double>("Mt_radio");

                    b.Property<string>("Type");

                    b.Property<double>("avance");

                    b.Property<DateTime>("date");

                    b.Property<DateTime>("dateDossier");

                    b.Property<DateTime?>("dateLunette");

                    b.Property<double>("diff");

                    b.Property<string>("employeematricule");

                    b.Property<string>("etat");

                    b.Property<double>("montant");

                    b.Property<double>("rembourse");

                    b.Property<long?>("rembourssementId");

                    b.HasKey("referance");

                    b.HasIndex("BordereauId");

                    b.HasIndex("ConjointId");

                    b.HasIndex("EnfantId");

                    b.HasIndex("employeematricule");

                    b.HasIndex("rembourssementId");

                    b.ToTable("Dossiers");
                });

            modelBuilder.Entity("CMIM.Models.Employee", b =>
                {
                    b.Property<string>("matricule")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PlaceplacdeId");

                    b.Property<long>("UserId");

                    b.Property<string>("adresse");

                    b.Property<string>("buId");

                    b.Property<string>("company");

                    b.Property<string>("first_name");

                    b.Property<string>("last_name");

                    b.Property<string>("matriculecmim");

                    b.HasKey("matricule");

                    b.HasIndex("PlaceplacdeId");

                    b.HasIndex("UserId");

                    b.HasIndex("buId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("CMIM.Models.Enfant", b =>
                {
                    b.Property<long>("EnfantId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ConjointId");

                    b.Property<DateTime>("DateNs");

                    b.Property<DateTime>("DateVs");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<string>("MatriculeCmim");

                    b.Property<string>("employeematricule");

                    b.HasKey("EnfantId");

                    b.HasIndex("ConjointId");

                    b.HasIndex("employeematricule");

                    b.ToTable("Enfant");
                });

            modelBuilder.Entity("CMIM.Models.Regul_Emp", b =>
                {
                    b.Property<long>("regul_empID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeMatricule");

                    b.Property<long?>("RégularisationRegularisationID");

                    b.Property<double>("Total");

                    b.HasKey("regul_empID");

                    b.HasIndex("EmployeeMatricule");

                    b.HasIndex("RégularisationRegularisationID");

                    b.ToTable("Regul_Emp");
                });

            modelBuilder.Entity("CMIM.Models.Regularisation", b =>
                {
                    b.Property<long>("RegularisationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Mois");

                    b.Property<double>("Total");

                    b.Property<DateTime>("dateRegularisation");

                    b.HasKey("RegularisationID");

                    b.ToTable("Regularisation");
                });

            modelBuilder.Entity("CMIM.Models.Rembouse", b =>
                {
                    b.Property<long>("rembourssementId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateRembourssement");

                    b.HasKey("rembourssementId");

                    b.ToTable("Remboursser");
                });

            modelBuilder.Entity("CMIM.Models.Rembouslist", b =>
                {
                    b.Property<long>("listId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dossierreferance");

                    b.Property<long>("RembouserembourssementId");

                    b.Property<double>("montant");

                    b.HasKey("listId");

                    b.HasIndex("Dossierreferance")
                        .IsUnique()
                        .HasFilter("[Dossierreferance] IS NOT NULL");

                    b.HasIndex("RembouserembourssementId");

                    b.ToTable("list");
                });

            modelBuilder.Entity("CMIM.Models.Site", b =>
                {
                    b.Property<long>("SiteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name");

                    b.HasKey("SiteId");

                    b.ToTable("sites");
                });

            modelBuilder.Entity("CMIM.Models.User", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<bool>("Etat");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.Property<long>("siteId");

                    b.HasKey("Id");

                    b.HasIndex("siteId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CMIM.Models.Conjoint", b =>
                {
                    b.HasOne("CMIM.Models.Employee", "Employee")
                        .WithMany("conjoints")
                        .HasForeignKey("Employeematricule");
                });

            modelBuilder.Entity("CMIM.Models.Dossier", b =>
                {
                    b.HasOne("CMIM.Models.Bordereau")
                        .WithMany("Dossiers")
                        .HasForeignKey("BordereauId");

                    b.HasOne("CMIM.Models.Conjoint", "conjoint")
                        .WithMany()
                        .HasForeignKey("ConjointId");

                    b.HasOne("CMIM.Models.Enfant", "enfant")
                        .WithMany()
                        .HasForeignKey("EnfantId");

                    b.HasOne("CMIM.Models.Employee", "employee")
                        .WithMany("dossies")
                        .HasForeignKey("employeematricule");

                    b.HasOne("CMIM.Models.Rembouse", "Rembouse")
                        .WithMany()
                        .HasForeignKey("rembourssementId");
                });

            modelBuilder.Entity("CMIM.Models.Employee", b =>
                {
                    b.HasOne("CMIM.Models.ActivityPlace", "Place")
                        .WithMany("Employees")
                        .HasForeignKey("PlaceplacdeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMIM.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMIM.Models.BU", "bu")
                        .WithMany("Employees")
                        .HasForeignKey("buId");
                });

            modelBuilder.Entity("CMIM.Models.Enfant", b =>
                {
                    b.HasOne("CMIM.Models.Conjoint", "Conjoint")
                        .WithMany("Enfants")
                        .HasForeignKey("ConjointId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMIM.Models.Employee", "Employee")
                        .WithMany("enfants")
                        .HasForeignKey("employeematricule");
                });

            modelBuilder.Entity("CMIM.Models.Regul_Emp", b =>
                {
                    b.HasOne("CMIM.Models.Employee", "Employee")
                        .WithMany("regul_emp")
                        .HasForeignKey("EmployeeMatricule");

                    b.HasOne("CMIM.Models.Regularisation", "Régularisation")
                        .WithMany("listDesRégularisation")
                        .HasForeignKey("RégularisationRegularisationID");
                });

            modelBuilder.Entity("CMIM.Models.Rembouslist", b =>
                {
                    b.HasOne("CMIM.Models.Dossier", "Dossier")
                        .WithOne("Rembouslist")
                        .HasForeignKey("CMIM.Models.Rembouslist", "Dossierreferance");

                    b.HasOne("CMIM.Models.Rembouse", "Rembouse")
                        .WithMany("list")
                        .HasForeignKey("RembouserembourssementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMIM.Models.User", b =>
                {
                    b.HasOne("CMIM.Models.Site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
