using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTablePostTransformerWithAdditionalColumnsAndAddFKWithPublisherAndRecived : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_transformers_publisher_id",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "publisher_id",
                table: "post_transformers");

            migrationBuilder.AddColumn<int>(
                name: "document_type",
                table: "post_transformers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "following_person",
                table: "post_transformers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "incoming_number",
                table: "post_transformers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "post_number",
                table: "post_transformers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "recived_by_name",
                table: "post_transformers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "incoming_number",
                table: "post_externals",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_published_id",
                table: "post_transformers",
                column: "published_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_published_id",
                table: "post_transformers",
                column: "published_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_published_id",
                table: "post_transformers");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_transformers_published_id",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "document_type",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "following_person",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "incoming_number",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "post_number",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "recived_by_name",
                table: "post_transformers");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "incoming_number",
                table: "post_externals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_publisher_id",
                table: "post_transformers",
                column: "publisher_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
