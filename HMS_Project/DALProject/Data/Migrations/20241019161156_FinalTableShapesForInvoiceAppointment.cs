using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalTableShapesForInvoiceAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ApointmentId",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "InvoicId",
                table: "Apointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apointments_InvoicId",
                table: "Apointments",
                column: "InvoicId",
                unique: true,
                filter: "[InvoicId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Invoices_InvoicId",
                table: "Apointments",
                column: "InvoicId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices",
                column: "ReceptionistUserId",
                principalTable: "Receptionists",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Invoices_InvoicId",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Apointments_InvoicId",
                table: "Apointments");

            migrationBuilder.DropColumn(
                name: "InvoicId",
                table: "Apointments");

            migrationBuilder.AddColumn<int>(
                name: "ApointmentId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices",
                column: "Id",
                principalTable: "Apointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Receptionists_ReceptionistUserId",
                table: "Invoices",
                column: "ReceptionistUserId",
                principalTable: "Receptionists",
                principalColumn: "UserId");
        }
    }
}
