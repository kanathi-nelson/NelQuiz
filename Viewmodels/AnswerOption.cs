using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Viewmodels
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public bool? IsWrongAnswer { get; set; }
        public bool? IsCorrectAnswer { get; set; }
        public string Name { get; set; }
    }
}
