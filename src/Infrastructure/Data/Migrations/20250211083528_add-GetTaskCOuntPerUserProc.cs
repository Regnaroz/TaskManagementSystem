using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addGetTaskCOuntPerUserProc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTaskCounts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTaskCounts");
        }
    }
}
