﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class vw_Messages
    {
        [Key]
        public int MessageID { get; set; }
           public string FirstName {get;set;}
           public string LastName {get;set;}          
           public string MessageDesc {get;set;}
           public int? Encounter_ID {get;set;}
           public DateTime? CreatedDate {get;set;}
           public DateTime? Modified_Date {get;set;}
           public int? Posted_By {get;set;}
           public int? Status_ID {get;set;}
           public string StatusDesc {get;set;}
           public DateTime? Date {get;set;}
           public DateTime? Date_Of_Birth {get;set;}
    }
}