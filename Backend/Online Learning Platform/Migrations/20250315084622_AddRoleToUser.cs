using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talent.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<UserRole>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: UserRole.Student);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
