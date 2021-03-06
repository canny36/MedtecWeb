﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
     [Table("Practice")]
    public class Practice
    {
        [Key]
        public int PracticeID { get; set; }

        public string PracticeName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipeCode { get; set; }
        public string NPI { get; set; }
        public string TAXID { get; set; }
        public int StatusID { get; set; }
    }
}