using NelQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Interfaces
{
    public interface GeneralInterface
    {

        Task<Questions> GetQuestionById(int? quizid);
        Task<ApplicationUser> GetLoggedinUser();
        bool Userexists(string email);
        int? GetTimeAssessmentById(int? assessmentid);
        //Task<Assessments> GetAssessmentByUserId(int? userid);
        Task<Assessments> GetAssessmentById(int? quizid);
    }
}
