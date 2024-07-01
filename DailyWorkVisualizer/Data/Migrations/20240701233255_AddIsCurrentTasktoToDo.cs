using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyWorkVisualizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCurrentTasktoToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCurrentTask",
                table: "ToDos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCurrentTask",
                table: "ToDos");
        }
    }
}
