using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NelQuiz.Migrations
{
    public partial class initialone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalQuestionPeriod");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Questions",
                newName: "AssessmentId");

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionPeriodInSeconds = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAssessment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AssessmentsId = table.Column<int>(type: "int", nullable: true),
                    TimeToComplete = table.Column<int>(type: "int", nullable: true),
                    TotalMarks = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssessment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAssessment_Assessments_AssessmentsId",
                        column: x => x.AssessmentsId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AssessmentId",
                table: "Questions",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessment_AssessmentsId",
                table: "UserAssessment",
                column: "AssessmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssessment_UserId",
                table: "UserAssessment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Assessments_AssessmentId",
                table: "Questions",
                column: "AssessmentId",
                principalTable: "Assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Assessments_AssessmentId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "UserAssessment");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AssessmentId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "AssessmentId",
                table: "Questions",
                newName: "AnswerId");

            migrationBuilder.CreateTable(
                name: "TotalQuestionPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeInSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalQuestionPeriod", x => x.Id);
                });
        }
    }
}
