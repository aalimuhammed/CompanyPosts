using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdFromBridgeTableSysUsersCompanyAndMakeFKsOnlyPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_sys_users_companies",
                table: "sys_users_companies");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "sys_users",
                newName: "user_name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sys_users_companies",
                table: "sys_users_companies",
                columns: new[] { "sys_user_id", "company_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_sys_users_companies",
                table: "sys_users_companies");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "sys_users",
                newName: "username");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sys_users_companies",
                table: "sys_users_companies",
                columns: new[] { "sys_user_id", "company_id", "id" });
        }
    }
}
