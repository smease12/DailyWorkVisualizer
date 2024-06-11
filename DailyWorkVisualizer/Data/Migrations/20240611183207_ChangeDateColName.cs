using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyWorkVisualizer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Commits",
                newName: "CommitDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommitDate",
                table: "Commits",
                newName: "DateTime");
        }
    }
}
