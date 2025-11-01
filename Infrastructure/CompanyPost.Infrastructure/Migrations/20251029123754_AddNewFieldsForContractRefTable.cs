using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsForContractRefTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "contract_date",
                table: "contract_refs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "contract_number",
                table: "contract_refs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "created_by_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "currency",
                table: "contract_refs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "contract_refs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "details",
                table: "contract_refs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "contract_refs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "person_org_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "purchase_order_ref",
                table: "contract_refs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "contract_refs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "work_type_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_contract_number",
                table: "contract_refs",
                column: "contract_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_created_by_id",
                table: "contract_refs",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_person_org_id",
                table: "contract_refs",
                column: "person_org_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_project_id",
                table: "contract_refs",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_purchase_order_ref",
                table: "contract_refs",
                column: "purchase_order_ref",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_work_type_id",
                table: "contract_refs",
                column: "work_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_person_org_id",
                table: "contract_refs",
                column: "person_org_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_project_id",
                table: "contract_refs",
                column: "project_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_sys_users_created_by_id",
                table: "contract_refs",
                column: "created_by_id",
                principalTable: "sys_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_work_types_work_type_id",
                table: "contract_refs",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_person_org_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_project_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_sys_users_created_by_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_work_types_work_type_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_contract_number",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_created_by_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_person_org_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_project_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_purchase_order_ref",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_work_type_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "contract_date",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "contract_number",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "department",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "details",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "person_org_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "purchase_order_ref",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "value",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "work_type_id",
                table: "contract_refs");
        }
    }
}
