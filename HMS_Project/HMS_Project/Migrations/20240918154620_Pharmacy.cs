using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Migrations
{
    /// <inheritdoc />
    public partial class Pharmacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Pharmacies",
                columns: table => new
                {
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.PharmacyID);
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
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_ActiveSubstance_ActiveSubstancesId",
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
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.MedicationCode);
                    table.ForeignKey(
                        name: "FK_Medication_Pharmacies_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID");
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    PharmacistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.PharmacistID);
                    table.ForeignKey(
                        name: "FK_Pharmacists_Pharmacies_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PharmacyID = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Pharmacies_PharmacyID",
                        column: x => x.PharmacyID,
                        principalTable: "Pharmacies",
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
                        name: "FK_ActiveSubstancePrescription_ActiveSubstance_ActiveSubstancesId",
                        column: x => x.ActiveSubstancesId,
                        principalTable: "ActiveSubstance",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSubstancePrescription_Prescriptions_PrescriptionsPrescriptionID",
                        column: x => x.PrescriptionsPrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ActiveSubstancePrescription_PrescriptionsPrescriptionID",
                table: "ActiveSubstancePrescription",
                column: "PrescriptionsPrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancesSideEffect_ActiveSubstancesID",
                table: "ActiveSubstancesSideEffect",
                column: "ActiveSubstancesID");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_PharmacyID",
                table: "Medication",
                column: "PharmacyID");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ActiveSubstancesId",
                table: "Patient",
                column: "ActiveSubstancesId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_MedicationMedicationCode",
                table: "PatientMedication",
                column: "MedicationMedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacyID",
                table: "Pharmacists",
                column: "PharmacyID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacyID",
                table: "Prescriptions",
                column: "PharmacyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSubstanceInteraction");

            migrationBuilder.DropTable(
                name: "ActiveSubstanceMedication");

            migrationBuilder.DropTable(
                name: "ActiveSubstancePrescription");

            migrationBuilder.DropTable(
                name: "ActiveSubstancesSideEffect");

            migrationBuilder.DropTable(
                name: "PatientMedication");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "ActiveSubstance");
        }
    }
}
