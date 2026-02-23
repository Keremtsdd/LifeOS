using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class TransitionToDynamicData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WeeklyGoals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Description", "IconUrl", "Name", "RequirementValue" },
                values: new object[,]
                {
                    { 1, "İlk aktiviteni tamamladın!", "medal_bronze.png", "İlk Adım", 1 },
                    { 2, "500 XP barajını aştın!", "medal_silver.png", "XP Avcısı", 500 },
                    { 3, "Haftalık hedefini tamamladın!", "medal_gold.png", "Haftalık Savaşçı", 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ColorHex", "CreatedDate", "Icon", "IsDeleted", "Name", "XPMultiplier" },
                values: new object[,]
                {
                    { 1, "#FF4B2B", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(2493), "fitness", false, "Physical", 1.2 },
                    { 2, "#AF40FF", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3183), "school", false, "Learning", 1.5 },
                    { 3, "#2196F3", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3184), "work", false, "Work", 1.0 },
                    { 4, "#4CAF50", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3185), "groups", false, "Social", 1.1000000000000001 },
                    { 5, "#FFC107", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3186), "self-improvement", false, "Mental", 1.3 },
                    { 6, "#E91E63", new DateTime(2026, 2, 21, 12, 26, 22, 848, DateTimeKind.Utc).AddTicks(3187), "palette", false, "Creative", 1.3999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CurrentLevelXP", "FullName", "IdentityId", "Level", "NextLevelXP", "TotalXP" },
                values: new object[,]
                {
                    { 1, 579, "Kerem Taşdemir", "26867e6c-bb13-4843-a261-8d0e03e0d038", 1, 1000, 579 },
                    { 3, 0, "Zeynep Kaya", "bot-002", 3, 3000, 2500 },
                    { 4, 0, "Mert Demir", "bot-003", 1, 1000, 300 },
                    { 5, 0, "Selin Aydın", "bot-004", 2, 2000, 1200 }
                });

            migrationBuilder.InsertData(
                table: "WeeklyGoals",
                columns: new[] { "Id", "CategoryId", "StartDate", "TargetMinutes", "UserId" },
                values: new object[] { 1, 2, new DateTime(2026, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), 300, "26867e6c-bb13-4843-a261-8d0e03e0d038" });
        }
    }
}
