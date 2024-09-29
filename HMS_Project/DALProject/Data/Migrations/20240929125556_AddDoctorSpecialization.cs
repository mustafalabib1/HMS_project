using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSpecializationLookup",
                columns: table => new
                {
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecializationLookup", x => x.Specialization); 
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialization",
                table: "Doctors",
                column: "Specialization");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_Specialization",
                table: "Doctors",
                column: "Specialization",
                principalTable: "DoctorSpecializationLookup",
                principalColumn: "Specialization",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecializationLookup_Specialization",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "DoctorSpecializationLookup");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Specialization",
                table: "Doctors");
        }
    }
}
