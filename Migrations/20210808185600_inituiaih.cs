using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NelQuiz.Migrations
{
    public partial class inituiaih : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_TimeToAnswer_TimeToAnswerId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_UserAnswers_AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_TimeToAnswerId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "IsCorrectAnswer",
                table: "UserAnswers");

            migrationBuilder.RenameColumn(
                name: "TimeToAnswerId",
                table: "UserAnswers",
                newName: "TotallTimeToAnswer");

            migrationBuilder.RenameColumn(
                name: "TimeTakenToAnswer",
                table: "UserAnswers",
                newName: "AverageTimeToAnswer");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "UserAnswers",
                newName: "AssessmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_AssessmentsId");

            migrationBuilder.CreateTable(
                name: "UserQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    IsCorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    TimeToAnswerId = table.Column<int>(type: "int", nullable: true),
                    TimeToComplete = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswers_Answeroptions_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answeroptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserQuestionAnswers_TimeToAnswer_TimeToAnswerId",
                        column: x => x.TimeToAnswerId,
                        principalTable: "TimeToAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswers_AnswerId",
                table: "UserQuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswers_QuestionId",
                table: "UserQuestionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswers_TimeToAnswerId",
                table: "UserQuestionAnswers",
                column: "TimeToAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionAnswers_UserId",
                table: "UserQuestionAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Assessments_AssessmentsId",
                table: "UserAnswers",
                column: "AssessmentsId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Assessments_AssessmentsId",
                table: "UserAnswers");

            migrationBuilder.DropTable(
                name: "UserQuestionAnswers");

            migrationBuilder.RenameColumn(
                name: "TotallTimeToAnswer",
                table: "UserAnswers",
                newName: "TimeToAnswerId");

            migrationBuilder.RenameColumn(
                name: "AverageTimeToAnswer",
                table: "UserAnswers",
                newName: "TimeTakenToAnswer");

            migrationBuilder.RenameColumn(
                name: "AssessmentsId",
                table: "UserAnswers",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_AssessmentsId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuestionId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "UserAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectAnswer",
                table: "UserAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AnswerId",
                table: "UserAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_TimeToAnswerId",
                table: "UserAnswers",
                column: "TimeToAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_TimeToAnswer_TimeToAnswerId",
                table: "UserAnswers",
                column: "TimeToAnswerId",
                principalTable: "TimeToAnswer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_UserAnswers_AnswerId",
                table: "UserAnswers",
                column: "AnswerId",
                principalTable: "UserAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
