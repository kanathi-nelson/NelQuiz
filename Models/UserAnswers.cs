using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace NelQuiz.Models
{
    public class UserAnswers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [ForeignKey("Assessments")]
        public int? AssessmentsId { get; set; }
        
        [ForeignKey("User")]
        public int? UserId{ get; set; }

        public int? AverageTimeToAnswer{ get; set; }
        public int? TotallTimeToAnswer{ get; set; }  
     


        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }
               
        public ApplicationUser User { get; set; }
        public Assessments Assessments { get; set; }
    }
}
