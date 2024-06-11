using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyWorkVisualizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDayofTheWeekToDayModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOftheWeek",
                table: "Days",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOftheWeek",
                table: "Days");
        }
    }
}
