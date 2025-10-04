using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropDeliveryPersonsFromPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_person_orgs_delivery_person_id",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "ix_posts_delivery_person_id",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "delivery_person_id",
                table: "posts");

            migrationBuilder.AddColumn<Guid>(
                name: "person_org_id",
                table: "posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_posts_person_org_id",
                table: "posts",
                column: "person_org_id");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_person_orgs_person_org_id",
                table: "posts",
                column: "person_org_id",
                principalTable: "person_orgs",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_posts_person_orgs_person_org_id",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "ix_posts_person_org_id",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "person_org_id",
                table: "posts");

            migrationBuilder.AddColumn<Guid>(
                name: "delivery_person_id",
                table: "posts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_posts_delivery_person_id",
                table: "posts",
                column: "delivery_person_id");

            migrationBuilder.AddForeignKey(
                name: "fk_posts_person_orgs_delivery_person_id",
                table: "posts",
                column: "delivery_person_id",
                principalTable: "person_orgs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
