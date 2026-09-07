using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelatedToIdFKColumnToIncomingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "related_to_id",
                table: "in_coming",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_related_to_id",
                table: "in_coming",
                column: "related_to_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_publishers_related_to_id",
                table: "in_coming",
                column: "related_to_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_publishers_related_to_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_related_to_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "related_to_id",
                table: "in_coming");
        }
    }
}
