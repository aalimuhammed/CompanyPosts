using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustFKInTablePostTransformerAndAddFileNameColumnInEachPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_transformer_attachments_post_externals_post_external_id",
                table: "post_transformer_attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformer_attachments_post_transformers_post_transfor",
                table: "post_transformer_attachments");

            migrationBuilder.DropIndex(
                name: "ix_post_transformer_attachments_post_external_id",
                table: "post_transformer_attachments");

            migrationBuilder.DropColumn(
                name: "post_external_id",
                table: "post_transformer_attachments");

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "post_transformer_attachments",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "post_internal_attachments",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "post_external_attachments",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformer_attachments_post_transformers_post_transfor",
                table: "post_transformer_attachments",
                column: "post_transformer_id",
                principalTable: "post_transformers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_transformer_attachments_post_transformers_post_transfor",
                table: "post_transformer_attachments");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "post_transformer_attachments");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "post_internal_attachments");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "post_external_attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "post_external_id",
                table: "post_transformer_attachments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformer_attachments_post_external_id",
                table: "post_transformer_attachments",
                column: "post_external_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformer_attachments_post_externals_post_external_id",
                table: "post_transformer_attachments",
                column: "post_external_id",
                principalTable: "post_externals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformer_attachments_post_transformers_post_transfor",
                table: "post_transformer_attachments",
                column: "post_transformer_id",
                principalTable: "post_transformers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
