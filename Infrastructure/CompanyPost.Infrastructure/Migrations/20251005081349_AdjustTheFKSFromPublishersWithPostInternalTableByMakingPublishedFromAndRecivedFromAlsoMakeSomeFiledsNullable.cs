using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTheFKSFromPublishersWithPostInternalTableByMakingPublishedFromAndRecivedFromAlsoMakeSomeFiledsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_internals_publishers_publisher_id",
                table: "post_internals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers");

            migrationBuilder.RenameColumn(
                name: "delivery_time",
                table: "post_transformers",
                newName: "delivery_date");

            migrationBuilder.RenameColumn(
                name: "publisher_id",
                table: "post_internals",
                newName: "recieved_from_id");

            migrationBuilder.RenameColumn(
                name: "delivery_time",
                table: "post_internals",
                newName: "delivery_date");

            migrationBuilder.RenameIndex(
                name: "ix_post_internals_publisher_id",
                table: "post_internals",
                newName: "ix_post_internals_recieved_from_id");

            migrationBuilder.RenameColumn(
                name: "delivery_time",
                table: "post_externals",
                newName: "delivery_date");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_transformers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_transformers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "recieved_from_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_internals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_internals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_externals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "post_externals",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_externals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "recieved_from_id",
                table: "post_externals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_recieved_from_id",
                table: "post_transformers",
                column: "recieved_from_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_published_id",
                table: "post_internals",
                column: "published_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_recieved_from_id",
                table: "post_externals",
                column: "recieved_from_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_internals_publishers_published_id",
                table: "post_internals",
                column: "published_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_internals_publishers_recieved_from_id",
                table: "post_internals",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers",
                column: "recieved_from_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_externals_publishers_recieved_from_id",
                table: "post_externals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_internals_publishers_published_id",
                table: "post_internals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_internals_publishers_recieved_from_id",
                table: "post_internals");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers");

            migrationBuilder.DropForeignKey(
                name: "fk_post_transformers_publishers_recieved_from_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_transformers_recieved_from_id",
                table: "post_transformers");

            migrationBuilder.DropIndex(
                name: "ix_post_internals_published_id",
                table: "post_internals");

            migrationBuilder.DropIndex(
                name: "ix_post_externals_recieved_from_id",
                table: "post_externals");

            migrationBuilder.DropColumn(
                name: "recieved_from_id",
                table: "post_transformers");

            migrationBuilder.DropColumn(
                name: "recieved_from_id",
                table: "post_externals");

            migrationBuilder.RenameColumn(
                name: "delivery_date",
                table: "post_transformers",
                newName: "delivery_time");

            migrationBuilder.RenameColumn(
                name: "recieved_from_id",
                table: "post_internals",
                newName: "publisher_id");

            migrationBuilder.RenameColumn(
                name: "delivery_date",
                table: "post_internals",
                newName: "delivery_time");

            migrationBuilder.RenameIndex(
                name: "ix_post_internals_recieved_from_id",
                table: "post_internals",
                newName: "ix_post_internals_publisher_id");

            migrationBuilder.RenameColumn(
                name: "delivery_date",
                table: "post_externals",
                newName: "delivery_time");

            migrationBuilder.UpdateData(
                table: "post_transformers",
                keyColumn: "subject",
                keyValue: null,
                column: "subject",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_transformers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "post_transformers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "post_transformers",
                keyColumn: "about_work",
                keyValue: null,
                column: "about_work",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_transformers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "post_internals",
                keyColumn: "subject",
                keyValue: null,
                column: "subject",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_internals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "post_internals",
                keyColumn: "about_work",
                keyValue: null,
                column: "about_work",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_internals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "post_externals",
                keyColumn: "subject",
                keyValue: null,
                column: "subject",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "post_externals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "post_externals",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "post_externals",
                keyColumn: "about_work",
                keyValue: null,
                column: "about_work",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "about_work",
                table: "post_externals",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_post_externals_publishers_publisher_id",
                table: "post_externals",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_internals_publishers_publisher_id",
                table: "post_internals",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_transformers_publishers_publisher_id",
                table: "post_transformers",
                column: "publisher_id",
                principalTable: "publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
