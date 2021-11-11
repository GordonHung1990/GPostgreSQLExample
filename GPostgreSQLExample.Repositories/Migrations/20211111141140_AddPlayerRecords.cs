using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPostgreSQLExample.Repositories.Migrations
{
    public partial class AddPlayerRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "playerRecords",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    old_data = table.Column<string>(type: "jsonb", nullable: false),
                    new_data = table.Column<string>(type: "jsonb", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playerRecords", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "playerRecords",
                schema: "main");
        }
    }
}
