using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesWithNamesPostInternalPostExternalPostTrasnformerAndPublisher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "delivery_methods");

            migrationBuilder.DropTable(
                name: "post_types");

            migrationBuilder.DropTable(
                name: "post_headers");

            migrationBuilder.DropColumn(
                name: "attachments",
                table: "contracts");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "contract_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    contract_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_contract_attachments_contracts_contract_id",
                        column: x => x.contract_id,
                        principalTable: "contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_department = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_project = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_supplier = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_externals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    serial_number = table.Column<int>(type: "int", nullable: false),
                    document_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    published_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    publisher_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    subject = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    about_work = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    document_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    delivery_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    delivery_methods = table.Column<int>(type: "int", nullable: false),
                    created_by_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_externals", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_externals_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_externals_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_externals_sys_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_internals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    serial_number = table.Column<int>(type: "int", nullable: false),
                    document_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    published_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    publisher_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    subject = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    about_work = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    document_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    delivery_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    delivery_methods = table.Column<int>(type: "int", nullable: false),
                    created_by_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_internals", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_internals_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_internals_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_internals_sys_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_transformers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    serial_number = table.Column<int>(type: "int", nullable: false),
                    document_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    published_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    publisher_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    subject = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    about_work = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    document_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    delivery_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    summary = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    delivery_methods = table.Column<int>(type: "int", nullable: false),
                    created_by_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_transformers", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_transformers_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_transformers_publishers_publisher_id",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_transformers_sys_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_external_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_external_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_external_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_external_attachments_post_externals_post_external_id",
                        column: x => x.post_external_id,
                        principalTable: "post_externals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_internal_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_internal_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_internal_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_internal_attachments_post_internals_post_internal_id",
                        column: x => x.post_internal_id,
                        principalTable: "post_internals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_transformer_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_transformer_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_external_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_transformer_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_transformer_attachments_post_externals_post_external_id",
                        column: x => x.post_external_id,
                        principalTable: "post_externals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_transformer_attachments_post_transformers_post_transfor",
                        column: x => x.post_transformer_id,
                        principalTable: "post_transformers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_companies_company_code",
                table: "companies",
                column: "company_code");

            migrationBuilder.CreateIndex(
                name: "ix_contract_attachments_contract_id",
                table: "contract_attachments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_external_attachments_post_external_id",
                table: "post_external_attachments",
                column: "post_external_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_company_id",
                table: "post_externals",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_created_by_id",
                table: "post_externals",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_externals_publisher_id",
                table: "post_externals",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internal_attachments_post_internal_id",
                table: "post_internal_attachments",
                column: "post_internal_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_company_id",
                table: "post_internals",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_created_by_id",
                table: "post_internals",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_internals_publisher_id",
                table: "post_internals",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformer_attachments_post_external_id",
                table: "post_transformer_attachments",
                column: "post_external_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformer_attachments_post_transformer_id",
                table: "post_transformer_attachments",
                column: "post_transformer_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_company_id",
                table: "post_transformers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_created_by_id",
                table: "post_transformers",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_transformers_publisher_id",
                table: "post_transformers",
                column: "publisher_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contract_attachments");

            migrationBuilder.DropTable(
                name: "post_external_attachments");

            migrationBuilder.DropTable(
                name: "post_internal_attachments");

            migrationBuilder.DropTable(
                name: "post_transformer_attachments");

            migrationBuilder.DropTable(
                name: "post_internals");

            migrationBuilder.DropTable(
                name: "post_externals");

            migrationBuilder.DropTable(
                name: "post_transformers");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.AddColumn<string>(
                name: "attachments",
                table: "contracts",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "delivery_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_methods", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_headers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_headers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_types_post_headers_post_id",
                        column: x => x.post_id,
                        principalTable: "post_headers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_by_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    delivery_method_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_header_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_original_sender_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    post_type_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    project_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    attachment = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_of_delivery = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    date_of_post = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    document_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    person_org_id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    serial_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subject = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_delivery_methods_delivery_method_id",
                        column: x => x.delivery_method_id,
                        principalTable: "delivery_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_person_orgs_person_org_id",
                        column: x => x.person_org_id,
                        principalTable: "person_orgs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_posts_person_orgs_post_original_sender_id",
                        column: x => x.post_original_sender_id,
                        principalTable: "person_orgs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_post_headers_post_header_id",
                        column: x => x.post_header_id,
                        principalTable: "post_headers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_post_types_post_type_id",
                        column: x => x.post_type_id,
                        principalTable: "post_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_posts_sys_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "sys_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_post_types_post_id",
                table: "post_types",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_created_by_id",
                table: "posts",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_delivery_method_id",
                table: "posts",
                column: "delivery_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_document_number",
                table: "posts",
                column: "document_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_posts_person_org_id",
                table: "posts",
                column: "person_org_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_post_header_id",
                table: "posts",
                column: "post_header_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_post_original_sender_id",
                table: "posts",
                column: "post_original_sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_post_type_id",
                table: "posts",
                column: "post_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_posts_project_id",
                table: "posts",
                column: "project_id");
        }
    }
}
