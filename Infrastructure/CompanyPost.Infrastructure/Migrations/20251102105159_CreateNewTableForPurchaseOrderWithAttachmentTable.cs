using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTableForPurchaseOrderWithAttachmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchase_orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    serial_number = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    details = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    purchase_order_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notes = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    purchase_order_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    project_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    person_org_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    currency = table.Column<int>(type: "int", nullable: false),
                    created_by_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    work_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_orders_publishers_person_org_id",
                        column: x => x.person_org_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_purchase_orders_publishers_project_id",
                        column: x => x.project_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_purchase_orders_sys_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_purchase_orders_work_types_work_type_id",
                        column: x => x.work_type_id,
                        principalTable: "work_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "purchase_order_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    purchase_order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    file_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_order_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchase_order_attachments_purchase_orders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_order_attachments_purchase_order_id",
                table: "purchase_order_attachments",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_created_by_id",
                table: "purchase_orders",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_person_org_id",
                table: "purchase_orders",
                column: "person_org_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_project_id",
                table: "purchase_orders",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_purchase_order_number",
                table: "purchase_orders",
                column: "purchase_order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_purchase_orders_work_type_id",
                table: "purchase_orders",
                column: "work_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchase_order_attachments");

            migrationBuilder.DropTable(
                name: "purchase_orders");
        }
    }
}
