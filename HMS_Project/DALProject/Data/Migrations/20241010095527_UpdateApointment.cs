using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "ApointmentId",
                table: "Prescriptions");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Apointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PrescriptionId",
                table: "Apointments",
                column: "PrescriptionId",
                unique: true,
                filter: "[PrescriptionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Prescriptions_PrescriptionId",
                table: "Apointments",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Prescriptions_PrescriptionId",
                table: "Apointments");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_PrescriptionId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Apointments");

            migrationBuilder.AddColumn<int>(
                name: "ApointmentId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments",
                column: "Id",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
