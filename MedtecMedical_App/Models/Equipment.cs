﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{

    [Table("Equipment")]
    public class Equipment
    {
        [Key]
        public int EquipID { get; set; }

        public int PracticeID { get; set; }
        public string Equip_Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public int Quantity { get; set; }
        public int QuantityonHand { get; set; }
        public int StatusID { get; set; }
        [NotMapped]
        public int AccesoryCount { get; set; }
    }
}