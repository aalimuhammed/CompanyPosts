using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintForContractAttachmentWithRefAndContractWhenDeletingCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_attachments_contracts_contract_id",
                table: "contract_attachments");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments",
                column: "contract_ref_id",
                principalTable: "contract_refs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_attachments_contracts_contract_id",
                table: "contract_attachments",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_contract_attachments_contracts_contract_id",
                table: "contract_attachments");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments",
                column: "contract_ref_id",
                principalTable: "contract_refs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_contract_attachments_contracts_contract_id",
                table: "contract_attachments",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
