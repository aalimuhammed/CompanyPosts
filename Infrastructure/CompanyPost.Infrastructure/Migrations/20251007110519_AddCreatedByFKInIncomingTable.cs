using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByFKInIncomingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by_id",
                table: "in_coming",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_in_coming_created_by_id",
                table: "in_coming",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_sys_users_created_by_id",
                table: "in_coming",
                column: "created_by_id",
                principalTable: "sys_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_sys_users_created_by_id",
                table: "in_coming");

            migrationBuilder.DropIndex(
                name: "ix_in_coming_created_by_id",
                table: "in_coming");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "in_coming");
        }
    }
}
