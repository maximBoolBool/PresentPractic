using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentPractic.Migrations
{
    /// <inheritdoc />
    public partial class add_user_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    is_delete = table.Column<bool>(type: "boolean", nullable: false),
                    create_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "presents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    present_name = table.Column<string>(type: "text", nullable: false),
                    present_description = table.Column<string>(type: "text", nullable: false),
                    present_status = table.Column<bool>(type: "boolean", nullable: false),
                    User = table.Column<Guid>(type: "uuid", nullable: false),
                    present_add_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presents", x => x.id);
                    table.ForeignKey(
                        name: "FK_presents_users_User",
                        column: x => x.User,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_presents_User",
                table: "presents",
                column: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "presents");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
