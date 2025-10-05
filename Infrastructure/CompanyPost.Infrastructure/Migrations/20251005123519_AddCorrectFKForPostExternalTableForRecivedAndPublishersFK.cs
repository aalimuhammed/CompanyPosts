using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectFKForPostExternalTableForRecivedAndPublishersFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_publisher_id",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "publisher_id",
                table: "post_externals");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_published_id",
                table: "post_externals",
                column: "published_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_published_id",
                table: "post_externals",
                column: "published_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_published_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_published_id",
                table: "post_externals");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id",
                table: "post_externals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_publisher_id",
                table: "post_externals",
                column: "publisher_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
