using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForClinicSpecilization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_ClinicSpecializationLookup_Specilization",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_Specilization",
                table: "Clinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicSpecializationLookup",
                table: "ClinicSpecializationLookup");

            migrationBuilder.DropColumn(
                name: "Specilization",
                table: "Clinics");

            migrationBuilder.RenameTable(
                name: "ClinicSpecializationLookup",
                newName: "ClinicsSpecializationLookups");

            migrationBuilder.AddColumn<int>(
                name: "ClinicSpecializationId",
                table: "Clinics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClinicsSpecializationLookups",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicsSpecializationLookups",
                table: "ClinicsSpecializationLookups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_ClinicSpecializationId",
                table: "Clinics",
                column: "ClinicSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicsSpecializationLookups_Specialization",
                table: "ClinicsSpecializationLookups",
                column: "Specialization",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_ClinicsSpecializationLookups_ClinicSpecializationId",
                table: "Clinics",
                column: "ClinicSpecializationId",
                principalTable: "ClinicsSpecializationLookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_ClinicsSpecializationLookups_ClinicSpecializationId",
                table: "Clinics");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_ClinicSpecializationId",
                table: "Clinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicsSpecializationLookups",
                table: "ClinicsSpecializationLookups");

            migrationBuilder.DropIndex(
                name: "IX_ClinicsSpecializationLookups_Specialization",
                table: "ClinicsSpecializationLookups");

            migrationBuilder.DropColumn(
                name: "ClinicSpecializationId",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClinicsSpecializationLookups");

            migrationBuilder.RenameTable(
                name: "ClinicsSpecializationLookups",
                newName: "ClinicSpecializationLookup");

            migrationBuilder.AddColumn<string>(
                name: "Specilization",
                table: "Clinics",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicSpecializationLookup",
                table: "ClinicSpecializationLookup",
                column: "Specialization");

            migrationBuilder.CreateIndex(
                name: "IX_Clinics_Specilization",
                table: "Clinics",
                column: "Specilization");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinics_ClinicSpecializationLookup_Specilization",
                table: "Clinics",
                column: "Specilization",
                principalTable: "ClinicSpecializationLookup",
                principalColumn: "Specialization",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
