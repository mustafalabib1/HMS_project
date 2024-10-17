using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class addapointmentidasforeignkeytotableinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ApointmentId",
                table: "Invoices",
                column: "ApointmentId",
                unique: true,
                filter: "[ApointmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Apointments_ApointmentId",
                table: "Invoices",
                column: "ApointmentId",
                principalTable: "Apointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_ApointmentId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ApointmentId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices",
                column: "Id",
                principalTable: "Apointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
