using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GPostgreSQLExample.Repositories.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "main",
                columns: table => new
                {
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modify_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    status = table.Column<short>(type: "smallint", nullable: false, comment: "狀態(0:停用、1:啟用、2:鎖住)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.player_id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerInfos",
                schema: "main",
                columns: table => new
                {
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    full_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    nick_name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mailbox = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("playerinfos_pk", x => x.player_id);
                    table.ForeignKey(
                        name: "playerinfos_fk",
                        column: x => x.player_id,
                        principalSchema: "main",
                        principalTable: "players",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "players_account_idx",
                schema: "main",
                table: "players",
                column: "account",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerInfos",
                schema: "main");

            migrationBuilder.DropTable(
                name: "players",
                schema: "main");
        }
    }
}