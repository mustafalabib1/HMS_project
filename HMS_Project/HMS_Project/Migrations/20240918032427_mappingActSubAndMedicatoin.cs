using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Migrations
{
    /// <inheritdoc />
    public partial class mappingActSubAndMedicatoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveSubstancesId",
                table: "HmsUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "HmsUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatAddress",
                table: "HmsUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "HmsUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActiveSubstance",
                columns: table => new
                {
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveSubstancesName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstance", x => x.ActiveSubstancesId);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.PharmacyID);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstanceInteraction",
                columns: table => new
                {
                    ActiveSubstanceId1 = table.Column<int>(type: "int", nullable: false),
                    ActiveSubstanceId2 = table.Column<int>(type: "int", nullable: false),
                    Interaction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstanceInteraction", x => new { x.ActiveSubstanceId1, x.ActiveSubstanceId2 });
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceInteraction_ActiveSubstance_ActiveSubstanceId1",
                        column: x => x.ActiveSubstanceId1,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId");
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceInteraction_ActiveSubstance_ActiveSubstanceId2",
                        column: x => x.ActiveSubstanceId2,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId");
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstancesSideEffect",
                columns: table => new
                {
                    SideEffects = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ActiveSubstancesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstancesSideEffect", x => new { x.SideEffects, x.ActiveSubstancesID });
                    table.ForeignKey(
                        name: "FK_ActiveSubstancesSideEffect_ActiveSubstance_ActiveSubstancesID",
                        column: x => x.ActiveSubstancesID,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId");
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescription_ActiveSubstance_ActiveSubstancesId",
                        column: x => x.ActiveSubstancesId,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId");
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    MedicationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    PharmacyID = table.Column<int>(type: "int", nullable: false),
                    PharmacyID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.MedicationCode);
                    table.ForeignKey(
                        name: "FK_Medication_Pharmacy_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyID");
                    table.ForeignKey(
                        name: "FK_Medication_Pharmacy_PharmacyID1",
                        column: x => x.PharmacyID1,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyID");
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstanceMedication",
                columns: table => new
                {
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: false),
                    MedicationCodesMedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstanceMedication", x => new { x.ActiveSubstancesId, x.MedicationCodesMedicationCode });
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceMedication_ActiveSubstance_ActiveSubstancesId",
                        column: x => x.ActiveSubstancesId,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceMedication_Medication_MedicationCodesMedicationCode",
                        column: x => x.MedicationCodesMedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMedication",
                columns: table => new
                {
                    PatientPatientId = table.Column<int>(type: "int", nullable: false),
                    MedicationMedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateIssued = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMedication", x => new { x.PatientPatientId, x.MedicationMedicationCode });
                    table.ForeignKey(
                        name: "FK_PatientMedication_Medication_MedicationMedicationCode",
                        column: x => x.MedicationMedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HmsUsers_ActiveSubstancesId",
                table: "HmsUsers",
                column: "ActiveSubstancesId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstance_ActiveSubstancesName",
                table: "ActiveSubstance",
                column: "ActiveSubstancesName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceInteraction_ActiveSubstanceId2",
                table: "ActiveSubstanceInteraction",
                column: "ActiveSubstanceId2");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceMedication_MedicationCodesMedicationCode",
                table: "ActiveSubstanceMedication",
                column: "MedicationCodesMedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancesSideEffect_ActiveSubstancesID",
                table: "ActiveSubstancesSideEffect",
                column: "ActiveSubstancesID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_PharmacyID",
                table: "Medication",
                column: "PharmacyID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_PharmacyID1",
                table: "Medication",
                column: "PharmacyID1");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_MedicationMedicationCode",
                table: "PatientMedication",
                column: "MedicationMedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_ActiveSubstancesId",
                table: "Prescription",
                column: "ActiveSubstancesId");

            migrationBuilder.AddForeignKey(
                name: "FK_HmsUsers_ActiveSubstance_ActiveSubstancesId",
                table: "HmsUsers",
                column: "ActiveSubstancesId",
                principalTable: "ActiveSubstance",
                principalColumn: "ActiveSubstancesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HmsUsers_ActiveSubstance_ActiveSubstancesId",
                table: "HmsUsers");

            migrationBuilder.DropTable(
                name: "ActiveSubstanceInteraction");

            migrationBuilder.DropTable(
                name: "ActiveSubstanceMedication");

            migrationBuilder.DropTable(
                name: "ActiveSubstancesSideEffect");

            migrationBuilder.DropTable(
                name: "PatientMedication");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "ActiveSubstance");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_HmsUsers_ActiveSubstancesId",
                table: "HmsUsers");

            migrationBuilder.DropColumn(
                name: "ActiveSubstancesId",
                table: "HmsUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "HmsUsers");

            migrationBuilder.DropColumn(
                name: "PatAddress",
                table: "HmsUsers");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "HmsUsers");
        }
    }
}
