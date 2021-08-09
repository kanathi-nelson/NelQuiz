using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NelQuiz.Models
{
    public class TimeToAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("From time (seconds)")]
        public int? StartTime { get; set; }
        [DisplayName("To time (seconds)")]
        public int? EndTime { get; set; }
      
        public string Meaning { get; set; }

        public string Description { get; set; }

        [DisplayName("Created Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Created Time")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }

        //public TransactionTypes TransactionTypes { get; set; }

        public ICollection<UserQuestionAnswers> UserQuestionAnswers { get; set; }
    }
}
