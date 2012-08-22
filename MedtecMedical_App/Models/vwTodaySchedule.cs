using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class vwTodaySchedule
    {
        [Key]
        public int? PatientID{get;set;}
        public int? PracticeID{get; set;}
        public string Patient {get; set;}
        public string Provider {get; set;}
        public int?  VisitID {get;set;}
        public DateTime? VisitDate{get;set;}
        public string New_Patient {get; set;}
        public string MD_Sign {get; set;}
        public string Patient_Sign {get; set;}
        public int? EncounterID{get;set;}
      
    }
}