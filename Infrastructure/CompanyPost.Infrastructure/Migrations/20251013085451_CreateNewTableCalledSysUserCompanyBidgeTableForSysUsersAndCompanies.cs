using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTableCalledSysUserCompanyBidgeTableForSysUsersAndCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_responsible_employee_employees_employee_id",
                table: "in_coming_responsible_employee");

            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_responsible_employee_in_coming_in_coming_id",
                table: "in_coming_responsible_employee");

            migrationBuilder.DropPrimaryKey(
                name: "pk_in_coming_responsible_employee",
                table: "in_coming_responsible_employee");

            migrationBuilder.RenameTable(
                name: "in_coming_responsible_employee",
                newName: "in_coming_responsible_employees");

            migrationBuilder.RenameIndex(
                name: "ix_in_coming_responsible_employee_in_coming_id",
                table: "in_coming_responsible_employees",
                newName: "ix_in_coming_responsible_employees_in_coming_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_in_coming_responsible_employees",
                table: "in_coming_responsible_employees",
                columns: new[] { "employee_id", "in_coming_id", "id" });

            migrationBuilder.CreateTable(
                name: "sys_users_companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    sys_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_responsible_employees_employees_employee_id",
                table: "in_coming_responsible_employees",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_responsible_employees_in_coming_in_coming_id",
                table: "in_coming_responsible_employees",
                column: "in_coming_id",
                principalTable: "in_coming",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_responsible_employees_employees_employee_id",
                table: "in_coming_responsible_employees");

            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_responsible_employees_in_coming_in_coming_id",
                table: "in_coming_responsible_employees");

            migrationBuilder.DropTable(
                name: "sys_users_companies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_in_coming_responsible_employees",
                table: "in_coming_responsible_employees");

            migrationBuilder.RenameTable(
                name: "in_coming_responsible_employees",
                newName: "in_coming_responsible_employee");

            migrationBuilder.RenameIndex(
                name: "ix_in_coming_responsible_employees_in_coming_id",
                table: "in_coming_responsible_employee",
                newName: "ix_in_coming_responsible_employee_in_coming_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_in_coming_responsible_employee",
                table: "in_coming_responsible_employee",
                columns: new[] { "employee_id", "in_coming_id", "id" });

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_responsible_employee_employees_employee_id",
                table: "in_coming_responsible_employee",
                column: "employee_id",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_responsible_employee_in_coming_in_coming_id",
                table: "in_coming_responsible_employee",
                column: "in_coming_id",
                principalTable: "in_coming",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
