using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class vwPdfGenEnconterAccess
    {     
            [Key]
           public int AccessoryID {get; set;}
           public int EncounterID { get; set; }
           public int Quantity {get; set;}
           public string PartNumber {get; set;}
           public int SupplyQuantity {get; set;}
           public int QuantityonHand {get; set;}
           public string Manufacturer {get; set;}
           public int StatusID {get; set;}
           public string AccessoryName {get; set;}
    }
}