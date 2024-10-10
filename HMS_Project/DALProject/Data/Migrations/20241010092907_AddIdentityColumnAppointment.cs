using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    public partial class AddIdentityColumnAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop foreign key constraints that reference the Id column
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            // Step 2: Drop the default constraint if it exists
            // You may need to adjust the name of the default constraint here
            var defaultConstraintName = "DF_Apointments_Id"; // Example name, change as needed

            migrationBuilder.Sql($"IF OBJECT_ID(N'dbo.{defaultConstraintName}', N'D') IS NOT NULL " +
                                 $"BEGIN " +
                                 $"ALTER TABLE [Apointments] DROP CONSTRAINT [{defaultConstraintName}]; " +
                                 $"END");

            // Step 3: Drop the primary key if Id is part of it
            migrationBuilder.DropPrimaryKey(
                name: "PK_Apointments",
                table: "Apointments");

            // Step 4: Add a temporary column to store the current Id values
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Apointments",
                nullable: false,
                defaultValue: 0);

            // Step 5: Copy the current Id values to TempId
            migrationBuilder.Sql("UPDATE Apointments SET TempId = Id");

            // Step 6: Drop the current Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Apointments");

            // Step 7: Recreate the Id column with IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Apointments",
                type: "int",
                nullable: false
            ).Annotation("SqlServer:Identity", "1, 1");

            // Step 8: Restore the Id values from TempId, if necessary
            // Note: This step is often not needed as you can't assign values to identity columns directly in SQL Server
            // If you want to preserve old IDs, you'll have to manage that differently

            // Step 9: Drop the temporary TempId column
            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Apointments");

            // Step 10: Recreate the primary key on the Id column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Apointments",
                table: "Apointments",
                column: "Id");

            // Step 11: Recreate foreign key constraints referencing the Id column
            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments",
                column: "Id",
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


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop foreign key constraints that reference the Id column
            migrationBuilder.DropForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Apointments_Id",
                table: "Invoices");

            // Step 2: Drop the default constraint if it exists
            // You may need to adjust the name of the default constraint here
            var defaultConstraintName = "DF_Apointments_Id"; // Example name, change as needed

            migrationBuilder.Sql($"IF OBJECT_ID(N'dbo.{defaultConstraintName}', N'D') IS NOT NULL " +
                                 $"BEGIN " +
                                 $"ALTER TABLE [Apointments] DROP CONSTRAINT [{defaultConstraintName}]; " +
                                 $"END");

            // Step 3: Drop the primary key if Id is part of it
            migrationBuilder.DropPrimaryKey(
                name: "PK_Apointments",
                table: "Apointments");

            // Step 4: Add a temporary column to store the current Id values
            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Apointments",
                nullable: false,
                defaultValue: 0);

            // Step 5: Copy the current Id values to TempId
            migrationBuilder.Sql("UPDATE Apointments SET TempId = Id");

            // Step 6: Drop the current Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Apointments");

            // Step 7: Recreate the Id column without the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Apointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Step 8: Restore the Id values from TempId
            migrationBuilder.Sql("UPDATE Apointments SET Id = TempId");

            // Step 9: Drop the temporary TempId column
            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Apointments");

            // Step 10: Recreate the primary key on the Id column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Apointments",
                table: "Apointments",
                column: "Id");

            // Step 11: Recreate foreign key constraints referencing the Id column
            migrationBuilder.AddForeignKey(
                name: "FK_Apointments_Prescriptions_Id",
                table: "Apointments",
                column: "Id",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
