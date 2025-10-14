using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseLengthOfWorkTypeNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_incoming_attachments_in_coming_incoming_id",
                table: "incoming_attachments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_incoming_attachments",
                table: "incoming_attachments");

            migrationBuilder.RenameTable(
                name: "incoming_attachments",
                newName: "in_coming_attachments");

            migrationBuilder.RenameIndex(
                name: "ix_incoming_attachments_incoming_id",
                table: "in_coming_attachments",
                newName: "ix_in_coming_attachments_incoming_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "work_types",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_in_coming_attachments",
                table: "in_coming_attachments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_in_coming_attachments_in_coming_incoming_id",
                table: "in_coming_attachments",
                column: "incoming_id",
                principalTable: "in_coming",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_in_coming_attachments_in_coming_incoming_id",
                table: "in_coming_attachments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_in_coming_attachments",
                table: "in_coming_attachments");

            migrationBuilder.RenameTable(
                name: "in_coming_attachments",
                newName: "incoming_attachments");

            migrationBuilder.RenameIndex(
                name: "ix_in_coming_attachments_incoming_id",
                table: "incoming_attachments",
                newName: "ix_incoming_attachments_incoming_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "work_types",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_incoming_attachments",
                table: "incoming_attachments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_incoming_attachments_in_coming_incoming_id",
                table: "incoming_attachments",
                column: "incoming_id",
                principalTable: "in_coming",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
