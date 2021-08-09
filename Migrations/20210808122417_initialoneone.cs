using Microsoft.EntityFrameworkCore.Migrations;

namespace NelQuiz.Migrations
{
    public partial class initialoneone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicsId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "TopicsId",
                table: "Questions",
                newName: "TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_TopicsId",
                table: "Questions",
                newName: "IX_Questions_TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "Questions",
                newName: "TopicsId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_TopicId",
                table: "Questions",
                newName: "IX_Questions_TopicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicsId",
                table: "Questions",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
