using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyWorkVisualizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDayModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Commits",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commits_DayId",
                table: "Commits",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Commits_DayId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Commits");
        }
    }
}
