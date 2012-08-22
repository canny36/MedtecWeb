using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    public class vwPdfGenEncounterInfo
    {
        public int PatientID {get; set;}
        [Key]
           public int EncounterID {get; set;}
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
           public int Est_Treatment_Dur {get; set;}
           public string HCPCS_Code {get; set;}
           public string Drug {get; set;}
           public string J_Code {get; set;}
           public int Ptn_Intravenous_Conti_Times {get; set;}
           public int Ptn_Intravenous_Conti_Days {get; set;}
           public bool Ptn_Continu_Administrat {get; set;}
           public int Ptn_Continu_Adminstrat_IFno {get; set;}
           public bool Ptn_Intravenous_Infusion { get; set; }
           public string Ptn_Presc_Of_Equip {get; set;}
           public string Notes {get; set;}
           public DateTime Start_Refill_Date {get; set;}
           public string Equip_Inspected_By {get; set;}
           public string Pii_Reason_PatientUnsign {get; set;}
           public string Pii_Guardian_Address1 {get; set;}
           public string Pii_Guardian_Relation {get; set;}
           public string Manufacturer {get; set;}
           public int Height {get; set;}
           public int Weight {get; set;}
          [DataType(DataType.Date)]
           public DateTime Date_Of_Birth {get; set;}
           public string Facility_Address {get; set;}
           public string Facility_Name {get; set;}
           public string Diagnosis_Codes {get; set;}
           public string Type_Of_Equip {get; set;}
           public string Sex {get; set;}
           public int EquipID {get; set;}
           public string Equip_Name {get; set;}
           public string Model {get; set;}
           public int Quantity {get; set;}
           public int QuantityonHand {get; set;}
           [NotMapped]
           public string FullName
           {
               get
               {
                   return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
               }
           }
           public string Mcr_Notes { get; set; }
           public int PracticeID {get; set;}
           public int ProviderID {get; set;}
           public string PracticeName {get; set;}
           public string Pract_Address1 {get; set;}
           public string Pract_Address2 {get; set;}
           public string NPI {get; set;}
           public byte[] Driver_Licence_Img { get; set; }
           public byte[] Po_Patient_Sign { get; set; }
           public byte[] Po_CompanyRep_Sign { get; set; }
           public byte[] Mcr_Beneficiary_Sign { get; set; }
           public byte[] Pii_Patient_Sign { get; set; }
           public byte[] Pii_Legalguardian_Sign { get; set; }
           public byte[] Ptn_Physician_Sign { get; set; }
           public byte[] Dmeif_Supplier_Sign { get; set; }

           public List<EquipmentAccessories> EncAcc = new List<EquipmentAccessories>();
    }
}