using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatePrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Prescriptions_PrescriptionId1",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_PrescriptionId1",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "Apointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices",
                column: "Id",
                principalTable: "Apointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId1",
                table: "Apointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_PrescriptionId1",
                table: "Apointments",
                column: "PrescriptionId1",
                unique: true,
                filter: "[PrescriptionId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Prescriptions_PrescriptionId1",
                table: "Apointments",
                column: "PrescriptionId1",
                principalTable: "Prescriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices",
                column: "Id",
                principalTable: "Apointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
