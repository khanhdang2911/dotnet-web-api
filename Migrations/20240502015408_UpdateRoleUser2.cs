using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_web_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Users_UsersId",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles");

            migrationBuilder.DropIndex(
                name: "IX_userRoles_UsersId",
                table: "userRoles");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "userRoles");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "userRoles",
                newName: "UsersID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles",
                columns: new[] { "UsersID", "RoleID" });

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Users_UsersID",
                table: "userRoles",
                column: "UsersID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Users_UsersID",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles");

            migrationBuilder.RenameColumn(
                name: "UsersID",
                table: "userRoles",
                newName: "UsersId");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "userRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_userRoles",
                table: "userRoles",
                columns: new[] { "UserID", "RoleID" });

            migrationBuilder.CreateIndex(
                name: "IX_userRoles_UsersId",
                table: "userRoles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Users_UsersId",
                table: "userRoles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
