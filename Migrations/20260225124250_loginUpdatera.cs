using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class loginUpdatera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserActivities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Users_UserId1",
                table: "UserActivities",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Users_UserId1",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId1",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserActivities");
        }
    }
}
