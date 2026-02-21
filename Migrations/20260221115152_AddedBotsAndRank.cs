using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class AddedBotsAndRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(3645));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(3646));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(3647));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 11, 51, 51, 897, DateTimeKind.Utc).AddTicks(3648));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CurrentLevelXP", "FullName", "IdentityId", "Level", "NextLevelXP", "TotalXP" },
                values: new object[,]
                {
                    { 3, 0, "Zeynep Kaya", "bot-002", 3, 3000, 2500 },
                    { 4, 0, "Mert Demir", "bot-003", 1, 1000, 300 },
                    { 5, 0, "Selin Aydın", "bot-004", 2, 2000, 1200 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7323));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7963));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7965));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7966));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 9, 44, 4, 431, DateTimeKind.Utc).AddTicks(7968));
        }
    }
}
