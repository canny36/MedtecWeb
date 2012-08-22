using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
     [Table("vwBillerMessages")]
    public class vwBillerMessages
    {
         [Key]
         public int PatientID { get; set; }
         public int PracticeID { get; set; }
         public string PatientName { get; set; }
         public int EncounterID { get; set; }
         public DateTime Date { get; set; }
         public string PracticeUserType { get; set;}
         public int Message_ID { get; set; }
         public string Message_Desc { get; set; }
         public string StatusCode { get; set; }
         public string StatusDesc { get; set; }
    }
}