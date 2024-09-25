using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class PresciptionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Reception_ReceptionId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Reception_ReceptionId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacists_Pharmacies_PharmacyId",
                table: "Pharmacists");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacyId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Receptionists_Reception_ReceptionID",
                table: "Receptionists");

            migrationBuilder.DropTable(
                name: "ActiveSubstancePrescription");

            migrationBuilder.DropTable(
                name: "MedicationPrescription");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Reception");

            migrationBuilder.DropIndex(
                name: "IX_Receptionists_ReceptionID",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PharmacyId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_PharmacyId",
                table: "Pharmacists");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReceptionId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_ReceptionId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "ReceptionID",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "DateIssued",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Dosage",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "ReceptionId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReceptionId",
                table: "Apointments");

            migrationBuilder.AddColumn<long>(
                name: "PharmacistId",
                table: "Prescriptions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReceptionistId",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReceptionistId",
                table: "Apointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PrescriptionItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullDosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActiveSubstanceId = table.Column<int>(type: "int", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionItem_ActiveSubstances_ActiveSubstanceId",
                        column: x => x.ActiveSubstanceId,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PrescriptionItem_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionItemMedication",
                columns: table => new
                {
                    MedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PrescriptionItemId = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItemMedication", x => new { x.MedicationCode, x.PrescriptionItemId });
                    table.ForeignKey(
                        name: "FK_PrescriptionItemMedication_Medication_MedicationCode",
                        column: x => x.MedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionItemMedication_PrescriptionItem_PrescriptionItemId",
                        column: x => x.PrescriptionItemId,
                        principalTable: "PrescriptionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionistId",
                table: "Invoices",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionistId",
                table: "Apointments",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItem_ActiveSubstanceId",
                table: "PrescriptionItem",
                column: "ActiveSubstanceId",
                unique: true,
                filter: "[ActiveSubstanceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItem_PrescriptionId",
                table: "PrescriptionItem",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItemMedication_PrescriptionItemId",
                table: "PrescriptionItemMedication",
                column: "PrescriptionItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistId",
                table: "Apointments",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "SSN",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistId",
                table: "Invoices",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "SSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId",
                principalTable: "Pharmacists",
                principalColumn: "SSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "PrescriptionItemMedication");

            migrationBuilder.DropTable(
                name: "PrescriptionItem");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_ReceptionistId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReceptionistId",
                table: "Apointments");

            migrationBuilder.AddColumn<int>(
                name: "ReceptionID",
                table: "Receptionists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateIssued",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Dosage",
                table: "Prescriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "Prescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "Pharmacists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceptionId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceptionId",
                table: "Apointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActiveSubstancePrescription",
                columns: table => new
                {
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionsPrescriptionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstancePrescription", x => new { x.ActiveSubstancesId, x.PrescriptionsPrescriptionID });
                    table.ForeignKey(
                        name: "FK_ActiveSubstancePrescription_ActiveSubstances_ActiveSubstancesId",
                        column: x => x.ActiveSubstancesId,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSubstancePrescription_Prescriptions_PrescriptionsPrescriptionID",
                        column: x => x.PrescriptionsPrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationPrescription",
                columns: table => new
                {
                    MedicationsMedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PrescriptionsPrescriptionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationPrescription", x => new { x.MedicationsMedicationCode, x.PrescriptionsPrescriptionID });
                    table.ForeignKey(
                        name: "FK_MedicationPrescription_Medication_MedicationsMedicationCode",
                        column: x => x.MedicationsMedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPrescription_Prescriptions_PrescriptionsPrescriptionID",
                        column: x => x.PrescriptionsPrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.PharmacyID);
                });

            migrationBuilder.CreateTable(
                name: "Reception",
                columns: table => new
                {
                    ReceptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reception", x => x.ReceptionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_ReceptionID",
                table: "Receptionists",
                column: "ReceptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacyId",
                table: "Prescriptions",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacyId",
                table: "Pharmacists",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionId",
                table: "Invoices",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionId",
                table: "Apointments",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancePrescription_PrescriptionsPrescriptionID",
                table: "ActiveSubstancePrescription",
                column: "PrescriptionsPrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescription_PrescriptionsPrescriptionID",
                table: "MedicationPrescription",
                column: "PrescriptionsPrescriptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Reception_ReceptionId",
                table: "Apointments",
                column: "ReceptionId",
                principalTable: "Reception",
                principalColumn: "ReceptionId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Reception_ReceptionId",
                table: "Invoices",
                column: "ReceptionId",
                principalTable: "Reception",
                principalColumn: "ReceptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacists_Pharmacies_PharmacyId",
                table: "Pharmacists",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "PharmacyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacyId",
                table: "Prescriptions",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "PharmacyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptionists_Reception_ReceptionID",
                table: "Receptionists",
                column: "ReceptionID",
                principalTable: "Reception",
                principalColumn: "ReceptionId");
        }
    }
}
