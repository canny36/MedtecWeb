﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedtecMedical_App.Models
{
    [Table("PatientEncounters")]
    public class PatientEncounters
    {
        [Key]
        public int EncounterID { get; set; }

        public int? PatientID { get; set; }


        public int? EquipID { get; set; }

        public int? ParentEncounterID { get; set; }


        public DateTime? Date { get; set; }

        public string Notes { get; set; }

        public string Equip_Options { get; set; }

        public string Presc_Physician { get; set; }

        public string Delivery_Method { get; set; }

        public DateTime? Start_Refill_Date { get; set; }

        public string Equip_Inspected_By { get; set; }

        public DateTime? Equip_Deliv_Date { get; set; }

        public string Facility_Name { get; set; }

        public string Facility_Address { get; set; }

        public string Diagnosis_Codes { get; set; }

        public int? Est_Treatment_Dur { get; set; }

        public string Equip_Serial_Num { get; set; }

        public string Type_Of_Equip { get; set; }

        public string Drug { get; set; }

        public string HCPCS_Code { get; set; }

        public string J_Code { get; set; }

        public byte[] Po_Patient_Sign { get; set; }


        public DateTime? Po_Patient_Sign_Date { get; set; }

        public byte[] Po_CompanyRep_Sign { get; set; }


        public DateTime? Po_Company_Rep_Sign_Date { get; set; }

        public DateTime? Po_Equip_Received_Date { get; set; }

        public byte[] Mcr_Beneficiary_Sign { get; set; }


        public DateTime? Mcr_Beneficiary_Sign_Date { get; set; }

        public string Mcr_Beneficiary_Name { get; set; }

        public string Mcr_Notes { get; set; }

        public byte[] Pdr_Patient_Sign { get; set; }


        public DateTime? Pdr_Patient_Sign_Date { get; set; }

        public byte[] Pdr_Legalguardian_Sign { get; set; }

        public string Pdr_Legalguardian_Name { get; set; }

        public byte[] Pii_Patient_Sign { get; set; }


        public DateTime? Pii_Patient_Sign_Date { get; set; }

        public byte[] Pii_Legalguardian_Sign { get; set; }

        public string Pii_Reason_PatientUnsign { get; set; }

        public string Pii_Guardian_Relation { get; set; }

        public string Pii_Guardian_Firstname { get; set; }

        public string Pii_Guardian_Lastname { get; set; }

        public string Pii_Guardian_Address1 { get; set; }

        public string Pii_Guardian_Address2 { get; set; }

        public string Pii_Guardian_City { get; set; }

        public string Pii_Guardian_State { get; set; }

        public string Pii_Guardian_Zip { get; set; }

        public string Pii_Guardian_Email { get; set; }

        public string Pii_Guardian_Phone { get; set; }

        public byte[] Ptn_Physician_Sign { get; set; }


        public DateTime? Ptn_Physician_Sign_Date { get; set; }

        public string Ptn_Physician_Name { get; set; }

        public int? Ptn_Intravenous_Conti_Times { get; set; }

        public int? Ptn_Intravenous_Conti_Days { get; set; }

        public bool? Ptn_Continu_Administrat { get; set; }

        public int? Ptn_Continu_Adminstrat_IFno { get; set; }

        public bool? Ptn_Intravenous_Infusion { get; set; }

        public string Ptn_Presc_Of_Equip { get; set; }

        public byte[] Dmeif_Supplier_Sign { get; set; }


        public DateTime? Dmeif_Supplier_Sign_Date { get; set; }


        public DateTime? Dmeif_Initial_Date { get; set; }

        public DateTime? Dmeif_Revised_Date { get; set; }

        public DateTime? Dmeif_Recertification_Date { get; set; }

        public int? StatusID { get; set; }
        public virtual Patient patnt { get; set; }

        public virtual Equipment equip { get; set; }
    }
}