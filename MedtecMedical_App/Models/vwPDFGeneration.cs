using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class vwPDFGeneration
    {

           public int PatientID {get; set;}
           public string FirstName {get; set;}
           public string LastName {get; set;}
           public string MiddleName {get; set;}
           public string Address1 {get; set;}
           public string City {get; set;}
           public string State {get; set;}
           public string Zip {get; set;}
           public string PhoneNumber1 {get; set;}
           public string PhoneNumber2 {get; set;}
           public string Emergency_Contact_Num {get; set;}
           public string HICN {get; set;}
           public string Insurance1ID {get; set;}
           public string PatientAcctNum {get; set;}
           public string Equip_Serial_Num {get; set;}
           public string Equip_Options {get; set;}
           public string Presc_Physician {get; set;}
           public string Delivery_Method {get; set;}
           public int? Est_Treatment_Dur {get; set;}
           public string HCPCS_Code {get; set;}
           public string Drug {get; set;}
           public string J_Code {get; set;}
           public int? Ptn_Intravenous_Conti_Times {get; set;}
           public int? Ptn_Intravenous_Conti_Days {get; set;}
           public int? Ptn_Continu_Administrat {get; set;}
           public int? Ptn_Continu_Adminstrat_IFno {get; set;}
           public int? Ptn_Intravenous_Infusion {get; set;}
           public string Ptn_Presc_Of_Equip {get; set;}
           public string Notes {get; set;}
           public DateTime Start_Refill_Date {get; set;}
           public string Equip_Inspected_By {get; set;}
           public string Pii_Reason_PatientUnsign {get; set;}
           public string Pii_Guardian_Address1 {get; set;}
           public string Pii_Guardian_Relation {get; set;}
           public string Manufacturer {get; set;}
           [Key]
           public int? EncounterID {get; set;}
           public int? EquipID {get; set;}
           public int? SupplyQuantity {get; set;}
           public int? QuantityonHand {get; set;}
           public string Expr1 {get; set;}
           public string AccessoryName {get; set;}
           public string PartNumber {get; set;}
           public int? AccessoryID {get; set;}
           [NotMapped]
           public string FullName
           {
               get
               {
                   return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
               }
           }
            public int? Height {get; set;}
            public int? Weight { get; set; }
            [DataType(DataType.Date)]
            public DateTime Date_Of_Birth { get; set; }
             public string Facility_Address {get; set;}
             public string Facility_Name { get; set; }
             public string Diagnosis_Codes { get; set; }
             public string Type_Of_Equip { get; set; }
             public string Sex { get; set; }
    }
}