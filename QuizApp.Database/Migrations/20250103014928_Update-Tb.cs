using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswers",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswers",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
