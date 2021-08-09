using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Models
{
    public class UserAssessment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        [ForeignKey("Assessments")]
        public int? AssessmentsId { get; set; }        
     
        public int? TimeToComplete { get; set; }

        public int? TotalMarks { get; set; }
        public int? CorrectQuizes { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }


        public Assessments Assessments { get; set; }
        public ApplicationUser User { get; set; }


    }
}
