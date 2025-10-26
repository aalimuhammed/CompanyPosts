using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkTypeIdInRemainingPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "post_internals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "post_externals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_work_type_id",
                table: "post_transformers",
                column: "work_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_work_type_id",
                table: "post_internals",
                column: "work_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_work_type_id",
                table: "post_externals",
                column: "work_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_work_types_work_type_id",
                table: "post_externals",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_internals_work_types_work_type_id",
                table: "post_internals",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_work_types_work_type_id",
                table: "post_transformers",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_work_types_work_type_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_internals_work_types_work_type_id",
                table: "post_internals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_work_types_work_type_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_transformers_work_type_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_internals_work_type_id",
                table: "post_internals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_work_type_id",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "post_internals");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "post_externals");
        }
    }
}
