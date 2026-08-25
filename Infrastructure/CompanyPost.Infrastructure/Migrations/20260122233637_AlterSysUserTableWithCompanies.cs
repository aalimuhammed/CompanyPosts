using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterSysUserTableWithCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_users_companies");

            migrationBuilder.AddColumn<Guid>(
                name: "company_id",
                table: "sys_users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "importing_status",
                table: "purchase_orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_sys_users_company_id",
                table: "sys_users",
                column: "company_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sys_users_companies_company_id",
                table: "sys_users",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sys_users_companies_company_id",
                table: "sys_users");

            migrationBuilder.DropIndex(
                name: "ix_sys_users_company_id",
                table: "sys_users");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "sys_users");

            migrationBuilder.DropColumn(
                name: "importing_status",
                table: "purchase_orders");

            migrationBuilder.CreateTable(
                name: "sys_users_companies",
                columns: table => new
                {
                    sys_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sys_users_companies", x => new { x.sys_user_id, x.company_id, x.id });
                    table.ForeignKey(
                        name: "fk_sys_users_companies_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sys_users_companies_sys_users_sys_user_id",
                        column: x => x.sys_user_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_sys_users_companies_company_id",
                table: "sys_users_companies",
                column: "company_id");
        }
    }
}
