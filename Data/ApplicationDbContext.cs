using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NelQuiz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NelQuiz.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
             
        }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answeroptions> Answeroptions { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<TimeToAnswer> TimeToAnswer { get; set; }
        public DbSet<UserQuestionAnswers> UserQuestionAnswers { get; set; }
        public DbSet<Assessments> Assessments { get; set; }
        public DbSet<UserAssessment> UserAssessment { get; set; }
        public DbSet<TopicResources> TopicResources { get; set; }
       
    }
}
