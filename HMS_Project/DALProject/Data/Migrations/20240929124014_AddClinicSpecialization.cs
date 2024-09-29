using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClinicSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specilization",
                table: "Clinics",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ClinicSpecializationLookup",
                columns: table => new
                {
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSpecializationLookup", x => x.Specialization);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinics_ClinicSpecializationLookup_Specilization",
                table: "Clinics");

            migrationBuilder.DropTable(
                name: "ClinicSpecializationLookup");

            migrationBuilder.DropIndex(
                name: "IX_Clinics_Specilization",
                table: "Clinics");

            migrationBuilder.AlterColumn<string>(
                name: "Specilization",
                table: "Clinics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");
        }
    }
}
