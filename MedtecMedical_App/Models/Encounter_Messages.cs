using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
     [Table("Encounter_Messages")]
    public class Encounter_Messages
    {
        [Key]
         public int Message_ID { get; set; }
          public string Message_Desc {get; set;}
           public int? Encounter_ID {get; set;}          
           public int? Status_ID {get; set;}
         [NotMapped]
           public string StatusDesc { get; set; }
         [NotMapped]
           public string FirstName { get; set; }
           [NotMapped]
           public string LastName { get; set; }
           [NotMapped]
           public DateTime? Date { get; set; }
           [NotMapped]
           public DateTime DOB { get; set; }
           public DateTime? Created_Date { get; set; }
           public DateTime? Modified_Date { get; set; }
           public int? Posted_By { get; set; }

    }
}