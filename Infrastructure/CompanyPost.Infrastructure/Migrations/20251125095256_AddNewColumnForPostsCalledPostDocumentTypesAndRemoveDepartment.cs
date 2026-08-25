using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnForPostsCalledPostDocumentTypesAndRemoveDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "document_type",
                table: "post_transformers",
                newName: "post_document_types");

            migrationBuilder.RenameColumn(
                name: "department",
                table: "post_internals",
                newName: "post_document_types");

            migrationBuilder.RenameColumn(
                name: "department",
                table: "in_coming",
                newName: "post_document_types");

            migrationBuilder.AddColumn<int>(
                name: "post_document_types",
                table: "post_externals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "post_document_types",
                table: "post_externals");

            migrationBuilder.RenameColumn(
                name: "post_document_types",
                table: "post_transformers",
                newName: "document_type");

            migrationBuilder.RenameColumn(
                name: "post_document_types",
                table: "post_internals",
                newName: "department");

            migrationBuilder.RenameColumn(
                name: "post_document_types",
                table: "in_coming",
                newName: "department");
        }
    }
}
