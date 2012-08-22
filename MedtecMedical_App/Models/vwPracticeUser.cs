using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    [Table("vwPracticeUser")]
    public class vwPracticeUser
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PracticeName { get; set; }
        public string Description { get; set; }
        public string PracticeUserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PracticeID { get; set; }
    }
}