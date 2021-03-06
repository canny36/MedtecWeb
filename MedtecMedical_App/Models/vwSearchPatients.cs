﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MedtecMedical_App.Models
{
    public class vwSearchPatients
    {
            [Key]
            public int? PatientID { get; set; }
            public int? PracticeID { get; set; }
            public int? ProviderID { get; set; }
            public string FirstName{ get; set; }
            public string LastName{ get; set; }
            public string PhoneNumber1{ get; set; }
            public DateTime? Date_Of_Birth{ get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }   
            public string InsuranceName{ get; set; }
            public int? Encounters {get;set;}
            public DateTime? LastVisit { get; set; }
            public int? Sub_Enc { get; set; }
        [NotMapped]
            public int? IncludeFilesFlag { get; set; }
           
                         
    }
}