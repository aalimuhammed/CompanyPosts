using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintForContractRefWhenDeletingContractDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_contracts_contract_id",
                table: "contract_refs");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_contracts_contract_id",
                table: "contract_refs",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_refs_contracts_contract_id",
                table: "contract_refs");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_refs_contracts_contract_id",
                table: "contract_refs",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
