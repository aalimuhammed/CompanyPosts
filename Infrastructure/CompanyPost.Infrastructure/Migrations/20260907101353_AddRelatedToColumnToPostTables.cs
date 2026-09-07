using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatedToColumnToPostTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "related_to_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "related_to_id",
                table: "post_internals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "related_to_id",
                table: "post_externals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_related_to_id",
                table: "post_transformers",
                column: "related_to_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_related_to_id",
                table: "post_internals",
                column: "related_to_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_related_to_id",
                table: "post_externals",
                column: "related_to_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_related_to_id",
                table: "post_externals",
                column: "related_to_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_internals_publishers_related_to_id",
                table: "post_internals",
                column: "related_to_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_related_to_id",
                table: "post_transformers",
                column: "related_to_id",
                principalTable: "publishers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_related_to_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_internals_publishers_related_to_id",
                table: "post_internals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_related_to_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_transformers_related_to_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_internals_related_to_id",
                table: "post_internals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_related_to_id",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "related_to_id",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "related_to_id",
                table: "post_internals");

            migrationBuilder.DropColumn(
                name: "related_to_id",
                table: "post_externals");
        }
    }
}
