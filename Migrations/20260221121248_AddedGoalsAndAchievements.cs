using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class AddedGoalsAndAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(3941));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(4569));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(4570));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(4571));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(4572));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 21, 12, 12, 47, 635, DateTimeKind.Utc).AddTicks(4573));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
