﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
        [Table("vwPatient_PatientEncounters")]
    public class vwPatient_PatientEncounters
    {
    
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Notes { get; set; }
        public int StatusID { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StatusDesc { get; set; }
        public int PracticeID { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Sex { get; set; }
        [Key]
        public int EncounterID { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
        public string Presc_Physician { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
            }
        }
    }
}   