using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyPost.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewTriggerAfterInsertingInTableBrdigeUsersToInsertInSysUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
                CREATE TRIGGER trg_BridgeUsers_Insert
                AFTER INSERT
                ON bridge_users
                FOR EACH ROW
                BEGIN
                  INSERT INTO sys_users (id, email, name, username, created_at)
                  VALUES (NEW.id, NEW.email, NEW.name, NEW.username, NEW.created_at);
                END;
                ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS trg_BridgeUsers_Insert;");
		}
    }
}
