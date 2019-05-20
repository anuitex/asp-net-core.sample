using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DataAccess.Migrations
{
    public partial class ChangedTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRefreshTokens_AspNetUsers_AccountId",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRefreshTokens",
                table: "AspNetRefreshTokens");

            migrationBuilder.RenameTable(
                name: "AspNetRefreshTokens",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRefreshTokens_AccountId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AccountId",
                table: "RefreshTokens",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AccountId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "AspNetRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "AspNetRefreshTokens",
                newName: "IX_AspNetRefreshTokens_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRefreshTokens",
                table: "AspNetRefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRefreshTokens_AspNetUsers_AccountId",
                table: "AspNetRefreshTokens",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
