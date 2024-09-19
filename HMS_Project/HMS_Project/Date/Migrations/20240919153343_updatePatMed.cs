using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS_Project.Date.Migrations
{
    /// <inheritdoc />
    public partial class updatePatMed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Patients_PatientId",
                table: "Medication");

            migrationBuilder.DropIndex(
                name: "IX_Medication_PatientId",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Medication");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedication_Patients_PatientPatientId",
                table: "PatientMedication",
                column: "PatientPatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedication_Patients_PatientPatientId",
                table: "PatientMedication");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Medication",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medication_PatientId",
                table: "Medication",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Patients_PatientId",
                table: "Medication",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId");
        }
    }
}
