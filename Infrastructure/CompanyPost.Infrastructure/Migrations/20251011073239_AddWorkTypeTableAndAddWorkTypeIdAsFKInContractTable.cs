using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkTypeTableAndAddWorkTypeIdAsFKInContractTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "contracts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "work_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_contracts_work_type_id",
                table: "contracts",
                column: "work_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_work_types_work_type_id",
                table: "contracts",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_work_types_work_type_id",
                table: "contracts");

            migrationBuilder.DropTable(
                name: "work_types");

            migrationBuilder.DropIndex(
                name: "ix_contracts_work_type_id",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "contracts");
        }
    }
}
