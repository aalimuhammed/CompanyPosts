using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectIdColumnFromIncomingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_publishers_project_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_project_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "in_coming");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "in_coming",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_project_id",
                table: "in_coming",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_publishers_project_id",
                table: "in_coming",
                column: "project_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
