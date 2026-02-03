using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropOriginalSenderFKFromInComingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_publishers_original_publisher_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_original_publisher_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "original_publisher_id",
                table: "in_coming");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id",
                table: "in_coming",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_publisher_id",
                table: "in_coming",
                column: "publisher_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_publishers_publisher_id",
                table: "in_coming",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_publishers_publisher_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_publisher_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "publisher_id",
                table: "in_coming");

            migrationBuilder.AddColumn<Guid>(
                name: "original_publisher_id",
                table: "in_coming",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_original_publisher_id",
                table: "in_coming",
                column: "original_publisher_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_publishers_original_publisher_id",
                table: "in_coming",
                column: "original_publisher_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
