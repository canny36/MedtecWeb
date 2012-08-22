using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    [Table("vwEquipmentAccessories")]
    public class vwEquipmentAccessories
    {
        [Key]
        public int AccessoryID { get; set; }
        public string AccessoryName { get; set; }
        public int EquipID { get; set; }        
        public int PracticeID { get; set; }
        public string Equip_Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int QuantityonHand { get; set; }
        public int StatusID { get; set; }
        public string PartNumber { get; set; }
        public int SupplyQuantity { get; set; }
        public int AccQtyOnHand { get; set; }
        public string AccManufacturer { get; set; }
        public int AccStatusID { get; set; }
        [NotMapped]
        public int AccessoryCount { get; set; }
    }
}