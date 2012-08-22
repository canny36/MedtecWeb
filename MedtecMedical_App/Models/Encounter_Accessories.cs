using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    [Table("Encounter_Accessories")]
    public class Encounter_Accessories
    {
        [Key]
        public int EncAccID { get; set; }

        public int EncounterID { get; set; }
        public int AccessoryID { get; set; }
        public int Quantity { get; set; }

        public virtual PatientEncounters patientenc { get; set; }
        public virtual EquipmentAccessories EquipAccess { get; set; }
    }
}