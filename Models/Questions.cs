using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NelQuiz.Models
{
    public class Questions
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Assessment")]
        [ForeignKey("Assessments")]
        public int? AssessmentId { get; set; }
        
        [ForeignKey("Topic")]
        public int? TopicId { get; set; }
        
      
        public string Name { get; set; }

        public string Description { get; set; }
        //public int? CorrectAnswerId { get; set; }
        public int QuizAnswers
        {
            get
            {
                return Answeroptions.Count();
            }
        }

        [DisplayName("Created Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Created Time")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }

        public Assessments Assessments { get; set; }
        public Topics Topic { get; set; }
        public ICollection<Answeroptions> Answeroptions { get; set; }
        public ICollection<UserQuestionAnswers> UserQuestionAnswers { get; set; }

    }
}
