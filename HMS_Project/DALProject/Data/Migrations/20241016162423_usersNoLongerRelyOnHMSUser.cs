using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class usersNoLongerRelyOnHMSUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Doctors_DoctorId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Patients_PatientId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorScheduleLookups_Doctors_DoctorId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Receptionists_Email",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Receptionists_UserId",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pharmacists",
                table: "Pharmacists");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_Email",
                table: "Pharmacists");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacists_UserId",
                table: "Pharmacists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_Email",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_UserId",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_DoctorScheduleLookups_DoctorId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Email",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_DoctorId",
                table: "Apointments");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_PatientId",
                table: "Apointments");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_ReceptionistId",
                table: "Apointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveSubstancePatient",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstancePatient_PatientshaveAllergyId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmacistId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Pharmacists");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "ReceptionistId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "ReceptionistId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PatientshaveAllergyId",
                table: "ActiveSubstancePatient");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Receptionists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PharmacistUserId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pharmacists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptionistUserId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "DoctorScheduleLookups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "Apointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientUserId",
                table: "Apointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceptionistUserId",
                table: "Apointments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pharmacists",
                table: "Pharmacists",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveSubstancePatient",
                table: "ActiveSubstancePatient",
                columns: new[] { "ActSbuAllergiesId", "PatientshaveAllergyUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorUserId",
                table: "Prescriptions",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacistUserId",
                table: "Prescriptions",
                column: "PharmacistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionistUserId",
                table: "Invoices",
                column: "ReceptionistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleLookups_DoctorUserId",
                table: "DoctorScheduleLookups",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_DoctorUserId",
                table: "Apointments",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PatientUserId",
                table: "Apointments",
                column: "PatientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionistUserId",
                table: "Apointments",
                column: "ReceptionistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancePatient_PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyUserId",
                principalTable: "Patients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Doctors_DoctorUserId",
                table: "Apointments",
                column: "DoctorUserId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Patients_PatientUserId",
                table: "Apointments",
                column: "PatientUserId",
                principalTable: "Patients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistUserId",
                table: "Apointments",
                column: "ReceptionistUserId",
                principalTable: "Receptionists",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorScheduleLookups_Doctors_DoctorUserId",
                table: "DoctorScheduleLookups",
                column: "DoctorUserId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices",
                column: "ReceptionistUserId",
                principalTable: "Receptionists",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorUserId",
                table: "Prescriptions",
                column: "DoctorUserId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistUserId",
                table: "Prescriptions",
                column: "PharmacistUserId",
                principalTable: "Pharmacists",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Doctors_DoctorUserId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Patients_PatientUserId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistUserId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorScheduleLookups_Doctors_DoctorUserId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorUserId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistUserId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorUserId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PharmacistUserId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pharmacists",
                table: "Pharmacists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReceptionistUserId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_DoctorScheduleLookups_DoctorUserId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_DoctorUserId",
                table: "Apointments");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_PatientUserId",
                table: "Apointments");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_ReceptionistUserId",
                table: "Apointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveSubstancePatient",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstancePatient_PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmacistUserId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "ReceptionistUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "DoctorScheduleLookups");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PatientUserId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "ReceptionistUserId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PatientshaveAllergyUserId",
                table: "ActiveSubstancePatient");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Receptionists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Receptionists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Receptionists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Receptionists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Receptionists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Receptionists",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Receptionists",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SSN",
                table: "Receptionists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PharmacistId",
                table: "Prescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pharmacists",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Pharmacists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Pharmacists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pharmacists",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Pharmacists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Pharmacists",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Pharmacists",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SSN",
                table: "Pharmacists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Patients",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SSN",
                table: "Patients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Nurses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Nurses",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Nurses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Nurses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Nurses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Nurses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SSN",
                table: "Nurses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ReceptionistId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "DoctorScheduleLookups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Doctors",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Doctors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Doctors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SSN",
                table: "Doctors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Apointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Apointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReceptionistId",
                table: "Apointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientshaveAllergyId",
                table: "ActiveSubstancePatient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receptionists",
                table: "Receptionists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pharmacists",
                table: "Pharmacists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveSubstancePatient",
                table: "ActiveSubstancePatient",
                columns: new[] { "ActSbuAllergiesId", "PatientshaveAllergyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_Email",
                table: "Receptionists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_UserId",
                table: "Receptionists",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_Email",
                table: "Pharmacists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_UserId",
                table: "Pharmacists",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Email",
                table: "Nurses",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_UserId",
                table: "Nurses",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReceptionistId",
                table: "Invoices",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorScheduleLookups_DoctorId",
                table: "DoctorScheduleLookups",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Email",
                table: "Doctors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_DoctorId",
                table: "Apointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PatientId",
                table: "Apointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_ReceptionistId",
                table: "Apointments",
                column: "ReceptionistId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstancePatient_PatientshaveAllergyId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstancePatient_Patients_PatientshaveAllergyId",
                table: "ActiveSubstancePatient",
                column: "PatientshaveAllergyId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Doctors_DoctorId",
                table: "Apointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Patients_PatientId",
                table: "Apointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Receptionists_ReceptionistId",
                table: "Apointments",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorScheduleLookups_Doctors_DoctorId",
                table: "DoctorScheduleLookups",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistId",
                table: "Invoices",
                column: "ReceptionistId",
                principalTable: "Receptionists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId",
                principalTable: "Pharmacists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
