using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkTypeIdFKInInComingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "in_coming",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_work_type_id",
                table: "in_coming",
                column: "work_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_work_types_work_type_id",
                table: "in_coming",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_work_types_work_type_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_work_type_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "in_coming");
        }
    }
}
