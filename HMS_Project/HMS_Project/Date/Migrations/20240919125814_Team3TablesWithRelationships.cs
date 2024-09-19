using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Date.Migrations
{
    /// <inheritdoc />
    public partial class Team3TablesWithRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstancePatient_Patient_PatientshaveAllergyPatientId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Patient_PatientId",
                table: "Medication");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patient_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Email",
                table: "Patients",
                newName: "IX_Patients_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "PatientId");

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagnosis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "GETDATE()"),
                    LabResults = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Apointments",
                columns: table => new
                {
                    ApointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApointmentTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ApointmentStatus = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false),
                    ReceptionId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apointments", x => x.ApointmentId);
                    table.ForeignKey(
                        name: "FK_Apointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apointments_Reception_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId",
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
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ReceptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Reception_ReceptionId",
                        column: x => x.ReceptionId,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receptionists",
                columns: table => new
                {
                    ReceptionistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptionID = table.Column<int>(type: "int", nullable: false),
                    SSN = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionists", x => x.ReceptionistID);
                    table.ForeignKey(
                        name: "FK_Receptionists_Reception_ReceptionID",
                        column: x => x.ReceptionID,
                        principalTable: "Reception",
                        principalColumn: "ReceptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PatientId",
                table: "Apointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionId",
                table: "Apointments",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatientId",
                table: "Invoices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionId",
                table: "Invoices",
                column: "ReceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientID",
                table: "MedicalRecords",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_Email",
                table: "Receptionists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_ReceptionID",
                table: "Receptionists",
                column: "ReceptionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyPatientId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyPatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Patients_PatientId",
                table: "Medication",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyPatientId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Patients_PatientId",
                table: "Medication");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_PatientId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Apointments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Receptionists");

            migrationBuilder.DropTable(
                name: "Reception");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_Email",
                table: "Patient",
                newName: "IX_Patient_Email");

            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstancePatient_Patient_PatientshaveAllergyPatientId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyPatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Patient_PatientId",
                table: "Medication",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patient_PatientId",
                table: "Prescriptions",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
