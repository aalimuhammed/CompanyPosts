using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsInPurchaseOrderCalledNatureOfWorksCheckValueAndCommercialRegisterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "old_reference_number",
                table: "purchase_orders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "check_value",
                table: "purchase_orders",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "commerical_register_id",
                table: "purchase_orders",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "nature_of_works",
                table: "purchase_orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "check_value",
                table: "purchase_orders");

            migrationBuilder.DropColumn(
                name: "commerical_register_id",
                table: "purchase_orders");

            migrationBuilder.DropColumn(
                name: "nature_of_works",
                table: "purchase_orders");

            migrationBuilder.AlterColumn<string>(
                name: "old_reference_number",
                table: "purchase_orders",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
