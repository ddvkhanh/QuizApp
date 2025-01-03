using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Questions",
                newName: "QuestionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionType",
                table: "Questions",
                newName: "Type");
        }
    }
}
