﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
     [Table("vwPractice")]
    public class vwPractice
    {
         [Key]
        public int PracticeID { get; set; }

        //public Practice practice { get; set; }
        public int UserID { get; set; }
       
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
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        public string PracticeUserType { get; set; }
      
    }
}