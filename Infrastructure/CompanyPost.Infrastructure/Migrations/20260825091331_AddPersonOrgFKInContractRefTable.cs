using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonOrgFKInContractRefTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_publisher_id1",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_publisher_id1",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "publisher_id1",
                table: "contract_refs");

            migrationBuilder.AddColumn<Guid>(
                name: "person_org_id",
                table: "contract_refs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_person_org_id",
                table: "contract_refs",
                column: "person_org_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_person_org_id",
                table: "contract_refs",
                column: "person_org_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_publishers_person_org_id",
                table: "contract_refs");

            migrationBuilder.DropIndex(
                name: "ix_contract_refs_person_org_id",
                table: "contract_refs");

            migrationBuilder.DropColumn(
                name: "person_org_id",
                table: "contract_refs");

            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id1",
                table: "contract_refs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_contract_refs_publisher_id1",
                table: "contract_refs",
                column: "publisher_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_publishers_publisher_id1",
                table: "contract_refs",
                column: "publisher_id1",
                principalTable: "publishers",
                principalColumn: "id");
        }
    }
}
