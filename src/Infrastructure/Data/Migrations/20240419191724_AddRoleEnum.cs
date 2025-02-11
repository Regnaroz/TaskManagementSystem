using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleProject.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnumRoleId",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnumRoleId",
                table: "AspNetRoles");
        }
    }
}
