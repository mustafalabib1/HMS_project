using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstanceMedication_Medication_MedicationsMedicationCode",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionItemMedication_Medication_MedicationCode",
                table: "PrescriptionItemMedication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medication",
                table: "Medication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveSubstanceMedication",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstanceMedication_MedicationsMedicationCode",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropColumn(
                name: "MedicationCode",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "MedicationsMedicationCode",
                table: "ActiveSubstanceMedication");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCode",
                table: "PrescriptionItemMedication",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "PrescriptionItemMedication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "Medication",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MedicationsMedicationId",
                table: "ActiveSubstanceMedication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medication",
                table: "Medication",
                column: "MedicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveSubstanceMedication",
                table: "ActiveSubstanceMedication",
                columns: new[] { "ActiveSubstancesId", "MedicationsMedicationId" });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItemMedication_MedicationId",
                table: "PrescriptionItemMedication",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceMedication_MedicationsMedicationId",
                table: "ActiveSubstanceMedication",
                column: "MedicationsMedicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstanceMedication_Medication_MedicationsMedicationId",
                table: "ActiveSubstanceMedication",
                column: "MedicationsMedicationId",
                principalTable: "Medication",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionItemMedication_Medication_MedicationId",
                table: "PrescriptionItemMedication",
                column: "MedicationId",
                principalTable: "Medication",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSubstanceMedication_Medication_MedicationsMedicationId",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionItemMedication_Medication_MedicationId",
                table: "PrescriptionItemMedication");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionItemMedication_MedicationId",
                table: "PrescriptionItemMedication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medication",
                table: "Medication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActiveSubstanceMedication",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSubstanceMedication_MedicationsMedicationId",
                table: "ActiveSubstanceMedication");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "PrescriptionItemMedication");

            migrationBuilder.DropColumn(
                name: "MedicationId",
                table: "Medication");

            migrationBuilder.DropColumn(
                name: "MedicationsMedicationId",
                table: "ActiveSubstanceMedication");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationCode",
                table: "PrescriptionItemMedication",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "MedicationCode",
                table: "Medication",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicationsMedicationCode",
                table: "ActiveSubstanceMedication",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medication",
                table: "Medication",
                column: "MedicationCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActiveSubstanceMedication",
                table: "ActiveSubstanceMedication",
                columns: new[] { "ActiveSubstancesId", "MedicationsMedicationCode" });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSubstanceMedication_MedicationsMedicationCode",
                table: "ActiveSubstanceMedication",
                column: "MedicationsMedicationCode");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSubstanceMedication_Medication_MedicationsMedicationCode",
                table: "ActiveSubstanceMedication",
                column: "MedicationsMedicationCode",
                principalTable: "Medication",
                principalColumn: "MedicationCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionItemMedication_Medication_MedicationCode",
                table: "PrescriptionItemMedication",
                column: "MedicationCode",
                principalTable: "Medication",
                principalColumn: "MedicationCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
