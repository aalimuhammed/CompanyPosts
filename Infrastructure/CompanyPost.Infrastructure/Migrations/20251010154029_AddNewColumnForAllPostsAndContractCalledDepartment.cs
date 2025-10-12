using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnForAllPostsAndContractCalledDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "post_transformers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "post_internals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "post_externals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "in_coming",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "department",
                table: "post_internals");

            migrationBuilder.DropColumn(
                name: "department",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "department",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "department",
                table: "contracts");
        }
    }
}
