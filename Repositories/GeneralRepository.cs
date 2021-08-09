using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NelQuiz.Data;
using NelQuiz.Interfaces;
using NelQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Repositories
{
    public class GeneralRepository :GeneralInterface
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Dictionary<int, string> MonthNames = new Dictionary<int, string>();

        public GeneralRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _Context = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetLoggedinUser()
        {
            var userloggedin = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return userloggedin;
        } 
        public async Task<Questions> GetQuestionById(int? quizid)
        {
            var userloggedin = await _Context.Questions.FirstOrDefaultAsync(i => i.Id == quizid);
            return userloggedin;
        }

        public async Task<Assessments> GetAssessmentById(int? quizid)
        {
            var userloggedin = await _Context.Assessments.FirstOrDefaultAsync(i => i.Id == quizid);
            return userloggedin;
        }
        public int? GetTimeAssessmentById(int? assessmentid)
        {
            var requiredtime = _Context.Assessments.FirstOrDefault(o => o.Id == assessmentid).QuestionPeriodInSeconds;
            return requiredtime;
        }
        //public async IEnumerable<Task<Assessments>> GetAssessmentByUserId(int? userid)
        //{
        //    var userloggedin = _Context.UserQuestionAnswers
        //        .Include(t => t.Answer)
        //        .Include(t => t.Question)
        //        .Include(t => t.User)
        //        .Include(t => t.TimeToAnswer)
        //        .Where(i => i.UserId == userid)
        //        //.Select(y=>y.QuestionId)
        //        .AsEnumerable();
        //    foreach(var a in userloggedin )
        //    {

        //    }
        //    return userloggedin;
        //}
        public bool Userexists(string email)
        {
            var newusa = _Context.Users.Where(p => p.Email == email).FirstOrDefault();
            if (newusa != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
