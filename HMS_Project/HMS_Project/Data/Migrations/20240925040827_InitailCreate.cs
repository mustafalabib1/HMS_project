using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveSubstances",
                columns: table => new
                {
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveSubstancesName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstances", x => x.ActiveSubstancesId);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specilization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.ClinicId);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    MedicationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.MedicationCode);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.SSN);
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

            migrationBuilder.CreateTable(
                name: "ActiveSubstanceInteraction",
                columns: table => new
                {
                    ActiveSubstanceId1 = table.Column<int>(type: "int", nullable: false),
                    ActiveSubstanceId2 = table.Column<int>(type: "int", nullable: false),
                    Interaction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstanceInteraction", x => new { x.ActiveSubstanceId1, x.ActiveSubstanceId2 });
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceInteraction_ActiveSubstances_ActiveSubstanceId1",
                        column: x => x.ActiveSubstanceId1,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId");
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceInteraction_ActiveSubstances_ActiveSubstanceId2",
                        column: x => x.ActiveSubstanceId2,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId");
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Doctors_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "ClinicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Nurses_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "ClinicId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstanceMedication",
                columns: table => new
                {
                    ActiveSubstancesId = table.Column<int>(type: "int", nullable: false),
                    MedicationsMedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstanceMedication", x => new { x.ActiveSubstancesId, x.MedicationsMedicationCode });
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceMedication_ActiveSubstances_ActiveSubstancesId",
                        column: x => x.ActiveSubstancesId,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSubstanceMedication_Medication_MedicationsMedicationCode",
                        column: x => x.MedicationsMedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicatoinSideEffect",
                columns: table => new
                {
                    SideEffects = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MedicationCode = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicatoinSideEffect", x => new { x.SideEffects, x.MedicationCode });
                    table.ForeignKey(
                        name: "FK_MedicatoinSideEffect_Medication_MedicationCode",
                        column: x => x.MedicationCode,
                        principalTable: "Medication",
                        principalColumn: "MedicationCode");
                });

            migrationBuilder.CreateTable(
                name: "ActiveSubstancePatient",
                columns: table => new
                {
                    ActSbuAllergiesActiveSubstancesId = table.Column<int>(type: "int", nullable: false),
                    PatientshaveAllergySSN = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSubstancePatient", x => new { x.ActSbuAllergiesActiveSubstancesId, x.PatientshaveAllergySSN });
                    table.ForeignKey(
                        name: "FK_ActiveSubstancePatient_ActiveSubstances_ActSbuAllergiesActiveSubstancesId",
                        column: x => x.ActSbuAllergiesActiveSubstancesId,
                        principalTable: "ActiveSubstances",
                        principalColumn: "ActiveSubstancesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergySSN",
                        column: x => x.PatientshaveAllergySSN,
                        principalTable: "Patients",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Pharmacists_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID");
                });

            migrationBuilder.CreateTable(
                name: "Receptionists",
                columns: table => new
                {
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    ReceptionID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionists", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Receptionists_Reception_ReceptionID",
                        column: x => x.ReceptionID,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId");
                });

            migrationBuilder.CreateTable(
                name: "DoctorScheduleLookups",
                columns: table => new
                {
                    Day = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorScheduleLookups", x => new { x.Day, x.DoctorId });
                    table.ForeignKey(
                        name: "FK_DoctorScheduleLookups_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dosage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    ApointmentId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "PharmacyID");
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
                name: "Apointments",
                columns: table => new
                {
                    ApointmentId = table.Column<int>(type: "int", nullable: false),
                    ApointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApointmentTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApointmentStatus = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    Examination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apointments", x => x.ApointmentId);
                    table.ForeignKey(
                        name: "FK_Apointments_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "ClinicId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Apointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apointments_Prescriptions_ApointmentId",
                        column: x => x.ApointmentId,
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionID");
                    table.ForeignKey(
                        name: "FK_Apointments_Reception_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId",
                        onDelete: ReferentialAction.SetNull);
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
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "GETDATE()"),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    ReceptionId = table.Column<int>(type: "int", nullable: true),
                    ApointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_Apointments_ApointmentId",
                        column: x => x.ApointmentId,
                        principalTable: "Apointments",
                        principalColumn: "ApointmentId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invoices_Reception_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceInteraction_ActiveSubstanceId2",
                table: "ActiveSubstanceInteraction",
                column: "ActiveSubstanceId2");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceMedication_MedicationsMedicationCode",
                table: "ActiveSubstanceMedication",
                column: "MedicationsMedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancePatient_PatientshaveAllergySSN",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergySSN");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancePrescription_PrescriptionsPrescriptionID",
                table: "ActiveSubstancePrescription",
                column: "PrescriptionsPrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstances_ActiveSubstancesName",
                table: "ActiveSubstances",
                column: "ActiveSubstancesName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ClinicId",
                table: "Apointments",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_DoctorId",
                table: "Apointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PatientId",
                table: "Apointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionId",
                table: "Apointments",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ClinicId",
                table: "Doctors",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleLookups_DoctorId",
                table: "DoctorScheduleLookups",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ApointmentId",
                table: "Invoices",
                column: "ApointmentId",
                unique: true,
                filter: "[ApointmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionId",
                table: "Invoices",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescription_PrescriptionsPrescriptionID",
                table: "MedicationPrescription",
                column: "PrescriptionsPrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicatoinSideEffect_MedicationCode",
                table: "MedicatoinSideEffect",
                column: "MedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_ClinicId",
                table: "Nurses",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Email",
                table: "Nurses",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_Email",
                table: "Pharmacists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_PharmacyId",
                table: "Pharmacists",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacyId",
                table: "Prescriptions",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_Email",
                table: "Receptionists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_ReceptionID",
                table: "Receptionists",
                column: "ReceptionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSubstanceInteraction");

            migrationBuilder.DropTable(
                name: "ActiveSubstanceMedication");

            migrationBuilder.DropTable(
                name: "ActiveSubstancePatient");

            migrationBuilder.DropTable(
                name: "ActiveSubstancePrescription");

            migrationBuilder.DropTable(
                name: "DoctorScheduleLookups");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MedicationPrescription");

            migrationBuilder.DropTable(
                name: "MedicatoinSideEffect");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "Receptionists");

            migrationBuilder.DropTable(
                name: "ActiveSubstances");

            migrationBuilder.DropTable(
                name: "Apointments");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Reception");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Clinics");
        }
    }
}
