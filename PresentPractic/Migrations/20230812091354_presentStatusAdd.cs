using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentPractic.Migrations
{
    /// <inheritdoc />
    public partial class presentStatusAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_presents_users_User",
                table: "presents");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "presents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_presents_User",
                table: "presents",
                newName: "IX_presents_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                table: "presents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_presents_users_UserId",
                table: "presents",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_presents_users_UserId",
                table: "presents");

            migrationBuilder.DropColumn(
                name: "is_delete",
                table: "presents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "presents",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_presents_UserId",
                table: "presents",
                newName: "IX_presents_User");

            migrationBuilder.AddForeignKey(
                name: "FK_presents_users_User",
                table: "presents",
                column: "User",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
