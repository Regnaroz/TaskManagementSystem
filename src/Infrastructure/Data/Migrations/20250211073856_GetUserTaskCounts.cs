using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class GetUserTaskCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE GetUserTaskCounts
                                                        AS
                                                        BEGIN
                                                            SELECT UserId, COUNT(*) AS TaskCount
                                                            FROM Tasks
                                                            GROUP BY UserId;
                                                        END;";
            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetUserTaskCounts");

        }
    }
}
