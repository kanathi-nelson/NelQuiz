using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Models
{
    public class UserQuestionAnswers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; } 
        
        [ForeignKey("Answer")]
        public int? AnswerId { get; set; }
        public bool IsCorrectAnswer { get; set; }


        [ForeignKey("TimeToAnswer")]
        public int? TimeToAnswerId { get; set; }        
     
        public int? TimeToComplete { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }


        public Answeroptions Answer { get; set; }
        public Questions Question { get; set; }
        public TimeToAnswer TimeToAnswer { get; set; }
        public ApplicationUser User { get; set; }


    }
}
