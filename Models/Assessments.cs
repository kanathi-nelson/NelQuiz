using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Models
{
    public class Assessments
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
      
        public string Name { get; set; }

        [DisplayName("Question Period In Seconds")]
        public int? QuestionPeriodInSeconds { get; set; }

        [DisplayName("Created Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Created Time")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<UserAnswers> UserAnswers { get; set; }
        public ICollection<UserAssessment> UserAssessments { get; set; }

    }
}
