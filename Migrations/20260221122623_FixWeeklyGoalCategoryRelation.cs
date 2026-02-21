using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class FixWeeklyGoalCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(2493));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3184));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3185));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3186));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3187));

            migrationBuilder.InsertData(
                table: "WeeklyGoals",
                columns: new[] { "Id", "CategoryId", "StartDate", "TargetMinutes", "UserId" },
                values: new object[] { 1, 2, new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), 300, "26867e6c-bb13-4843-a261-8d0e03e0d038" });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyGoals_CategoryId",
                table: "WeeklyGoals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyGoals_Categories_CategoryId",
                table: "WeeklyGoals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyGoals_Categories_CategoryId",
                table: "WeeklyGoals");

            migrationBuilder.DropIndex(
                name: "IX_WeeklyGoals_CategoryId",
                table: "WeeklyGoals");

            migrationBuilder.DeleteData(
                table: "WeeklyGoals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2773));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2774));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2775));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2776));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 23, 3, 592, DateTimeKind.Utc).AddTicks(2777));
        }
    }
}
