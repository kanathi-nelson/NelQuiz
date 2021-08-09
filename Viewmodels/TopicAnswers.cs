using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Viewmodels
{
    public class TopicAnswers
    {
        public string TopicName { get; set; }
        public int? TotalQuizes { get; set; }
        public double? Percentage { get; set; }
        public int? CorrectAnswers { get; set; }
       
    }
}
