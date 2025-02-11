using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGetTasksByStatusSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE GetTasksByStatus
                                            @Status INT,
                                            @UserId NVARCHAR(50)
                                        AS
                                        BEGIN
                                            SET NOCOUNT ON;

                                            IF @UserId is null
                                            BEGIN
                                                -- Admin can see all tasks with the given status
                                                SELECT * FROM Tasks WHERE Status = @Status;
                                            END
                                            ELSE
                                            BEGIN
                                                -- Non-admin users see only their own tasks
                                                SELECT * FROM Tasks WHERE Status = @Status AND UserId = @UserId;
                                            END
                                        END";
            migrationBuilder.Sql(procedure);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTasksByStatus");

        }
    }
}
