using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGetTasksDueToday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE GetTasksDueToday
                                                        AS
                                                        BEGIN
                                                            SET NOCOUNT ON;

                                                            SELECT * 
                                                            FROM Tasks
                                                            WHERE CAST(DueDate AS DATE) = CAST(GETDATE() AS DATE);
                                                        END;";
            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTasksDueToday");

        }
    }
}
