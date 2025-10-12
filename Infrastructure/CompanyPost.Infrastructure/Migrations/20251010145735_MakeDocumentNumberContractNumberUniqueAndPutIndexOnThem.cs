using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeDocumentNumberContractNumberUniqueAndPutIndexOnThem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_transformers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_internals",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_externals",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_document_number",
                table: "post_transformers",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_document_number",
                table: "post_internals",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_document_number",
                table: "post_externals",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_document_number",
                table: "in_coming",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contracts_contract_number",
                table: "contracts",
                column: "contract_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_post_transformers_document_number",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_internals_document_number",
                table: "post_internals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_document_number",
                table: "post_externals");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_document_number",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_contracts_contract_number",
                table: "contracts");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_transformers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_internals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "post_externals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
