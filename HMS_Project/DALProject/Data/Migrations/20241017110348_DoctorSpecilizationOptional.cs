using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSpecilizationOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_SpecializationId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "DoctorSpecializationLookup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_SpecializationId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "DoctorSpecializationLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
