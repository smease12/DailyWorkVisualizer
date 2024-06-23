using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyWorkVisualizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDonetoToDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits");

            migrationBuilder.AddColumn<bool>(
                name: "isDone",
                table: "ToDos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "Commits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "isDone",
                table: "ToDos");

            migrationBuilder.AlterColumn<int>(
                name: "DayId",
                table: "Commits",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Days_DayId",
                table: "Commits",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");
        }
    }
}
