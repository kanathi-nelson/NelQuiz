using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Models
{


    public class ApplicationUser : IdentityUser<int>
    {

       
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateofBirth { get; set; }

        public string PhoneNo { get; set; }   
      
       
        public string ConfirmationToken { get; set; }
        public string ResetToken { get; set; }


        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime CreatedTime { get; set; } 

        
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ModifiedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? ModifiedTime { get; set; }


        public bool Deleted { get; set; }

        public bool Blocked { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BlockedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? BlockedTime { get; set; }

        public int? BlockedById { get; set; }
      
        public int? ModifiedBy { get; set; }

        [ForeignKey("ApplicationRole")]
        public int? RoleId { get; set; }

        
        public virtual ICollection<UserAnswers> UserAnswers { get; set; }
        public ICollection<UserQuestionAnswers> UserQuestionAnswers { get; set; }
        public virtual ICollection<UserAssessment> UserAssessments { get; set; }
       
    }


}
