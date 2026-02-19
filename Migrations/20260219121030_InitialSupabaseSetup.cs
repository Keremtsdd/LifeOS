using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LifeOS.Migrations
{
    /// <inheritdoc />
    public partial class InitialSupabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    ColorHex = table.Column<string>(type: "text", nullable: false),
                    XPMultiplier = table.Column<double>(type: "double precision", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    EarnedXP = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActivities_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ColorHex", "CreatedDate", "Icon", "IsDeleted", "Name", "XPMultiplier" },
                values: new object[,]
                {
                    { 1, "#FF4B2B", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(744), "fitness", false, "Physical", 1.2 },
                    { 2, "#AF40FF", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1513), "school", false, "Learning", 1.5 },
                    { 3, "#2196F3", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1515), "work", false, "Work", 1.0 },
                    { 4, "#4CAF50", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1516), "groups", false, "Social", 1.1000000000000001 },
                    { 5, "#FFC107", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1517), "self-improvement", false, "Mental", 1.3 },
                    { 6, "#E91E63", new DateTime(2026, 2, 19, 12, 10, 29, 765, DateTimeKind.Utc).AddTicks(1518), "palette", false, "Creative", 1.3999999999999999 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_CategoryId",
                table: "UserActivities",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
