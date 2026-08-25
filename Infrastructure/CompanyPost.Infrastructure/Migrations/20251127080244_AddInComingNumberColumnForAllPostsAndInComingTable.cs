using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInComingNumberColumnForAllPostsAndInComingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "post_externals");

            migrationBuilder.AddColumn<string>(
                name: "in_coming_number",
                table: "post_transformers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "in_coming_number",
                table: "post_internals",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "in_coming_number",
                table: "post_externals",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "in_coming_number",
                table: "in_coming",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "in_coming_number",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "in_coming_number",
                table: "post_internals");

            migrationBuilder.DropColumn(
                name: "in_coming_number",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "in_coming_number",
                table: "in_coming");

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "post_externals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
