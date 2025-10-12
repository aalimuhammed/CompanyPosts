using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivedFromFKInTablePostExternalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals");

            migrationBuilder.RenameColumn(
                name: "recieved_from_id",
                table: "post_externals",
                newName: "received_from_supplier_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_externals_recieved_from_id",
                table: "post_externals",
                newName: "ix_post_externals_received_from_supplier_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_received_from_supplier_id",
                table: "post_externals",
                column: "received_from_supplier_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_received_from_supplier_id",
                table: "post_externals");

            migrationBuilder.RenameColumn(
                name: "received_from_supplier_id",
                table: "post_externals",
                newName: "recieved_from_id");

            migrationBuilder.RenameIndex(
                name: "ix_post_externals_received_from_supplier_id",
                table: "post_externals",
                newName: "ix_post_externals_recieved_from_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
