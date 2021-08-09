using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Viewmodels
{
    public class AssessmentViewmodel
    {
        public int QuestionId { get; set; }
        public int? Assessmentid { get; set; }
        public int? Timetoanswer { get; set; }
        public string QuestionName { get; set; }
        public List<AnswerOption> AnswerOptions { get; set; }
        public int ChosenAnswerId { get; set; }
    }
}
