using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustContractRefTableByRemovingDuplicateFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_person_org_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_project_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_work_types_work_type_id",
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

            migrationBuilder.DropColumn(
                name: "contract_number_ref",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "department",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "work_type_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id1",
                table: "contract_refs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_publisher_id",
                table: "contract_refs",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_publisher_id1",
                table: "contract_refs",
                column: "publisher_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_publisher_id",
                table: "contract_refs",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_publisher_id1",
                table: "contract_refs",
                column: "publisher_id1",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_work_types_work_type_id",
                table: "contract_refs",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_publisher_id",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_publisher_id1",
                table: "contract_refs");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_work_types_work_type_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_publisher_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_publisher_id1",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "publisher_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "publisher_id1",
                table: "contract_refs");

            migrationBuilder.AlterColumn<Guid>(
                name: "work_type_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "contract_number_ref",
                table: "contract_refs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "department",
                table: "contract_refs",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "fk_contract_refs_work_types_work_type_id",
                table: "contract_refs",
                column: "work_type_id",
                principalTable: "work_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
