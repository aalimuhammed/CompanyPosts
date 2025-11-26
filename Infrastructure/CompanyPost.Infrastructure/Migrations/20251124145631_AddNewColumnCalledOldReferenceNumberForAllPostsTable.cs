using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnCalledOldReferenceNumberForAllPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "old_reference_number",
                table: "post_transformers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "old_reference_number",
                table: "post_internals",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "old_reference_number",
                table: "post_externals",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "old_reference_number",
                table: "in_coming",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "old_reference_number",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "old_reference_number",
                table: "post_internals");

            migrationBuilder.DropColumn(
                name: "old_reference_number",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "old_reference_number",
                table: "in_coming");
        }
    }
}
