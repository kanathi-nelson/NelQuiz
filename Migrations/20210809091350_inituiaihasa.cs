using Microsoft.EntityFrameworkCore.Migrations;

namespace NelQuiz.Migrations
{
    public partial class inituiaihasa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectQuizes",
                table: "UserAssessment",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectQuizes",
                table: "UserAssessment");
        }
    }
}
