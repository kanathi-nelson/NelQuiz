﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NelQuiz.Data;

namespace NelQuiz.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210809134405_initialdata")]
    partial class initialdata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NelQuiz.Models.Answeroptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrectAnswer")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answeroptions");
                });

            modelBuilder.Entity("NelQuiz.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedByid")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("NelQuiz.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<int?>("BlockedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BlockedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("BlockedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateofBirth")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("NelQuiz.Models.Assessments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuestionPeriodInSeconds")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("NelQuiz.Models.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssessmentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("TopicId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("NelQuiz.Models.TimeToAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EndTime")
                        .HasColumnType("int");

                    b.Property<string>("Meaning")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StartTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeToAnswer");
                });

            modelBuilder.Entity("NelQuiz.Models.TopicResources", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicResources");
                });

            modelBuilder.Entity("NelQuiz.Models.Topics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("NelQuiz.Models.UserAnswers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssessmentsId")
                        .HasColumnType("int");

                    b.Property<int?>("AverageTimeToAnswer")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotallTimeToAnswer")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentsId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("NelQuiz.Models.UserAssessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssessmentsId")
                        .HasColumnType("int");

                    b.Property<int?>("CorrectQuizes")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TimeToComplete")
                        .HasColumnType("int");

                    b.Property<int?>("TotalMarks")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentsId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAssessment");
                });

            modelBuilder.Entity("NelQuiz.Models.UserQuestionAnswers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnswerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCorrectAnswer")
                        .HasColumnType("bit");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeToAnswerId")
                        .HasColumnType("int");

                    b.Property<int?>("TimeToComplete")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TimeToAnswerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserQuestionAnswers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("NelQuiz.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("NelQuiz.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("NelQuiz.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("NelQuiz.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NelQuiz.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("NelQuiz.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NelQuiz.Models.Answeroptions", b =>
                {
                    b.HasOne("NelQuiz.Models.Questions", "Question")
                        .WithMany("Answeroptions")
                        .HasForeignKey("QuestionId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("NelQuiz.Models.Questions", b =>
                {
                    b.HasOne("NelQuiz.Models.Assessments", "Assessments")
                        .WithMany("Questions")
                        .HasForeignKey("AssessmentId");

                    b.HasOne("NelQuiz.Models.Topics", "Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId");

                    b.Navigation("Assessments");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("NelQuiz.Models.TopicResources", b =>
                {
                    b.HasOne("NelQuiz.Models.Topics", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("NelQuiz.Models.UserAnswers", b =>
                {
                    b.HasOne("NelQuiz.Models.Assessments", "Assessments")
                        .WithMany("UserAnswers")
                        .HasForeignKey("AssessmentsId");

                    b.HasOne("NelQuiz.Models.ApplicationUser", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId");

                    b.Navigation("Assessments");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NelQuiz.Models.UserAssessment", b =>
                {
                    b.HasOne("NelQuiz.Models.Assessments", "Assessments")
                        .WithMany("UserAssessments")
                        .HasForeignKey("AssessmentsId");

                    b.HasOne("NelQuiz.Models.ApplicationUser", "User")
                        .WithMany("UserAssessments")
                        .HasForeignKey("UserId");

                    b.Navigation("Assessments");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NelQuiz.Models.UserQuestionAnswers", b =>
                {
                    b.HasOne("NelQuiz.Models.Answeroptions", "Answer")
                        .WithMany("UserQuestionAnswers")
                        .HasForeignKey("AnswerId");

                    b.HasOne("NelQuiz.Models.Questions", "Question")
                        .WithMany("UserQuestionAnswers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("NelQuiz.Models.TimeToAnswer", "TimeToAnswer")
                        .WithMany("UserQuestionAnswers")
                        .HasForeignKey("TimeToAnswerId");

                    b.HasOne("NelQuiz.Models.ApplicationUser", "User")
                        .WithMany("UserQuestionAnswers")
                        .HasForeignKey("UserId");

                    b.Navigation("Answer");

                    b.Navigation("Question");

                    b.Navigation("TimeToAnswer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NelQuiz.Models.Answeroptions", b =>
                {
                    b.Navigation("UserQuestionAnswers");
                });

            modelBuilder.Entity("NelQuiz.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserAnswers");

                    b.Navigation("UserAssessments");

                    b.Navigation("UserQuestionAnswers");
                });

            modelBuilder.Entity("NelQuiz.Models.Assessments", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserAssessments");
                });

            modelBuilder.Entity("NelQuiz.Models.Questions", b =>
                {
                    b.Navigation("Answeroptions");

                    b.Navigation("UserQuestionAnswers");
                });

            modelBuilder.Entity("NelQuiz.Models.TimeToAnswer", b =>
                {
                    b.Navigation("UserQuestionAnswers");
                });

            modelBuilder.Entity("NelQuiz.Models.Topics", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
