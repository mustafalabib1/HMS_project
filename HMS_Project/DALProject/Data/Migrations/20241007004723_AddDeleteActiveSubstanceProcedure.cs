using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DALProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteActiveSubstanceProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creating the stored procedure
            var procedure = @"CREATE PROCEDURE [dbo].[sp_DeleteActiveSubstance]
                                  @ActiveSubstancesId INT
                              AS
                              BEGIN
                                  -- Delete from the junction table where the substance is involved
                                  DELETE FROM ActiveSubstanceInteraction
                                  WHERE ActiveSubstanceId1 = @ActiveSubstancesId
                                     OR ActiveSubstanceId2 = @ActiveSubstancesId;
                              
                                  -- Then delete the substance itself
                                  DELETE FROM ActiveSubstances
                                  WHERE Id = @ActiveSubstancesId;
                              END";

            migrationBuilder.Sql(procedure);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the stored procedure if it exists
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteActiveSubstance]");

        }
    }
}
