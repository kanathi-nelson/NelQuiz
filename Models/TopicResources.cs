using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace NelQuiz.Models
{
    public class TopicResources
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }      
       
        [ForeignKey("Topic")]
        public int? TopicId { get; set; }

        public string Name{ get; set; }
        public string ResourceUrl{ get; set; }
        
        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM}")]
        public DateTime? CreatedTime { get; set; }
               
        public Topics Topic { get; set; }
    }
}
