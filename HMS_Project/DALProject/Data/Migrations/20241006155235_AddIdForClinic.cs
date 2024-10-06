using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClinicId",
                table: "Clinics",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActiveSubstanceInteraction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActiveSubstanceInteraction");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clinics",
                newName: "ClinicId");
        }
    }
}
