using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFKForContractRefIdInTableContractAttachmentAndMakeFKNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "contract_id",
                table: "contract_attachments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "contract_ref_id",
                table: "contract_attachments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_contract_attachments_contract_ref_id",
                table: "contract_attachments",
                column: "contract_ref_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments",
                column: "contract_ref_id",
                principalTable: "contract_refs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contract_attachments_contract_refs_contract_ref_id",
                table: "contract_attachments");

            migrationBuilder.DropIndex(
                name: "ix_contract_attachments_contract_ref_id",
                table: "contract_attachments");

            migrationBuilder.DropColumn(
                name: "contract_ref_id",
                table: "contract_attachments");

            migrationBuilder.AlterColumn<Guid>(
                name: "contract_id",
                table: "contract_attachments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
