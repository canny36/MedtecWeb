using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    [Table("PracticeUser")]
    public class PracticeUser
    {
        [Key]
        public int UserID { get; set; }
        public int PracticeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PracticeUserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NPI { get; set; }
        public int StatusID { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        //public virtual ICollection<Practice> Practices { get; set; }

    }
 
}