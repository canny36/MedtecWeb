using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class PatientVisits
    {
         [Key]
        public int?  VisitID {get; set;}
        public int ? PatientID {get; set;}
        public DateTime? VisitDate {get;set;}
        public string VisitTime {get; set;}
        public int ? ProviderID {get; set;}
        public int ? VisitCounter {get; set;}
        public int ? VisitCreatedBy {get; set;}
        public DateTime? VisitCreatedOn {get; set;}
        public int ? VisitModifiedBy {get; set;}
        public DateTime? VisitModifiedOn {get; set;}
     
    }
}