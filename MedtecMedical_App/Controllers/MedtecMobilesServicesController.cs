using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class MedtecMobilesServicesController : Controller
    {
        //
        // GET: /MedtecMobilesServices/

        MedtecContext objDBContext;

        public MedtecMobilesServicesController()
        {
            objDBContext = new MedtecContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var varPractice = (from p in objDBContext.vwPracticeUsers
                               select p).ToList();
            return Json(varPractice, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Practice Users Login Check
        /// </summary>
        /// <param name="objUsers"></param>
        /// <returns></returns>
        public JsonResult CheckUserlogins(UsersInfo objUsers)
        {
            var varPractice = (from p in objDBContext.vwPracticeUsers
                               where p.UserName == objUsers.UserName
                               where p.Password == objUsers.Password
                               where p.PracticeName == objUsers.PracticeName                               
                               select p).ToList();

            return Json(varPractice, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CreateNewPatient(Patient objPatient,int? Visit)
        {
            string meg = "";
            try
            {
                Patient objNewPatient = new Patient
                {
                    PracticeID = objPatient.PracticeID,
                    PatientAcctNum = objPatient.PatientAcctNum,
                    ProviderID = objPatient.ProviderID,
                    FirstName = objPatient.FirstName,
                    LastName = objPatient.LastName,
                    MiddleName = objPatient.MiddleName,
                    Email = objPatient.Email,
                    PhoneNumber1 = objPatient.PhoneNumber1,
                    PhoneNumber2 = objPatient.PhoneNumber2,
                    Emergency_Contact_Num = objPatient.Emergency_Contact_Num,
                    Age = objPatient.Age,
                    Sex = objPatient.Sex,
                    Address1 = objPatient.Address1,
                    Address2 = objPatient.Address2,
                    City = objPatient.City,
                    State = objPatient.State,
                    Zip = objPatient.Zip,
                    Date_Of_Birth = objPatient.Date_Of_Birth,
                    Insurance1ID = objPatient.Insurance1ID,
                    Sub1ID = objPatient.Sub1ID,
                    Sub1FirstName = objPatient.Sub1FirstName,
                    Sub1LastName = objPatient.Sub1LastName,
                    Insurance2ID = objPatient.Insurance2ID,
                    Sub2ID = objPatient.Sub2ID,
                    Sub2FirstName = objPatient.Sub2FirstName,
                    Sub2LastName = objPatient.Sub2LastName,
                    Insurance3ID = objPatient.Insurance3ID,
                    Sub3ID = objPatient.Sub3ID,
                    Sub3FirstName = objPatient.Sub3FirstName,
                    Sub3LastName = objPatient.Sub3LastName,
                    HICN = objPatient.HICN,
                    Height = objPatient.Height,
                    Weight = objPatient.Weight,
                    Ptype = objPatient.Ptype,
                    Guarantor1ID = objPatient.Guarantor1ID,
                    Guarantor2ID = objPatient.Guarantor2ID,
                    Guarantor3ID = objPatient.Guarantor3ID,
                    Driver_Licence_Img = objPatient.Driver_Licence_Img,
                    InsuranceType = objPatient.InsuranceType,
                    SelfPay = objPatient.SelfPay,
                    StatusID = 1
                };

              
                int? providerID = objPatient.ProviderID;
                objDBContext.Patients.Add(objNewPatient);
                objDBContext.SaveChanges();
                if (Visit == 1)
                {
                    int? lastPatientID = objDBContext.Patients.Max(Item => Item.PatientID);
                    meg = addPatientvisits(lastPatientID, providerID);
                }
                else
                {
                    meg = "Success";
                }
                //
               
            }
            catch (Exception ex)
            {
                meg = "Failed";
                meg = ex.Message;
            }
            return Json(new { result = meg }, JsonRequestBehavior.AllowGet);           
        }


        

        public JsonResult EditPatientInfo(Patient objPatient,int? Visit)
        {
            Patient objEditPatient = (from p in objDBContext.Patients
                                      where p.PatientID == objPatient.PatientID
                                      select p).FirstOrDefault();

            string meg = "";
            try
            {

                objEditPatient.PracticeID = objPatient.PracticeID;
                objEditPatient.ProviderID = objPatient.ProviderID;
                objEditPatient.PatientAcctNum = objPatient.PatientAcctNum;
                objEditPatient.FirstName = objPatient.FirstName;
                objEditPatient.LastName = objPatient.LastName;
                objEditPatient.MiddleName = objPatient.MiddleName;
                objEditPatient.Email = objPatient.Email;
                objEditPatient.PhoneNumber1 = objPatient.PhoneNumber1;
                objEditPatient.PhoneNumber2 = objPatient.PhoneNumber2;
                objEditPatient.Emergency_Contact_Num = objPatient.Emergency_Contact_Num;
                objEditPatient.Age = objPatient.Age;
                objEditPatient.Sex = objPatient.Sex;
                objEditPatient.Address1 = objPatient.Address1;
                objEditPatient.Address2 = objPatient.Address2;
                objEditPatient.City = objPatient.City;
                objEditPatient.State = objPatient.State;
                objEditPatient.Zip = objPatient.Zip;
                objEditPatient.Date_Of_Birth = objPatient.Date_Of_Birth;
                objEditPatient.Insurance1ID = objPatient.Insurance1ID;
                objEditPatient.Sub1ID = objPatient.Sub1ID;
                objEditPatient.Sub1FirstName = objPatient.Sub1FirstName;
                objEditPatient.Sub1LastName = objPatient.Sub1LastName;
                objEditPatient.Insurance2ID = objPatient.Insurance2ID;
                objEditPatient.Sub2ID = objPatient.Sub2ID;
                objEditPatient.Sub2FirstName = objPatient.Sub2FirstName;
                objEditPatient.Sub2LastName = objPatient.Sub2LastName;
                objEditPatient.Insurance3ID = objPatient.Insurance3ID;
                objEditPatient.Sub3ID = objPatient.Sub3ID;
                objEditPatient.Sub3FirstName = objPatient.Sub3FirstName;
                objEditPatient.Sub3LastName = objPatient.Sub3LastName;
                objEditPatient.HICN = objPatient.HICN;
                objEditPatient.Height = objPatient.Height;
                objEditPatient.Weight = objPatient.Weight;
                objEditPatient.Ptype = objPatient.Ptype;
                objEditPatient.Guarantor1ID = objPatient.Guarantor1ID;
                objEditPatient.Guarantor2ID = objPatient.Guarantor2ID;
                objEditPatient.Guarantor3ID = objPatient.Guarantor3ID;
                objEditPatient.Driver_Licence_Img = objPatient.Driver_Licence_Img;
                objEditPatient.InsuranceType = objPatient.InsuranceType;
                objEditPatient.SelfPay = objPatient.SelfPay;
                objEditPatient.StatusID = 1;
                objDBContext.SaveChanges();
                if (Visit == 1)
                {
                  meg =  addPatientvisits(objPatient.PatientID,objPatient.ProviderID);
                }
                else
                {
                    meg = "Success";
                }
                meg = "Success";
            }
            catch (Exception ex)
            {
                meg = "Failed";
                meg = ex.Message;
            }
            return Json(new { result = meg }, JsonRequestBehavior.AllowGet);
        }


        private string addPatientvisits(int? PatientID, int? ProviderID)
        {
            string visitcount = (from p in objDBContext.PatientVisitsList
                              where p.PatientID == PatientID
                              select p.VisitCounter).FirstOrDefault().ToString();


            PatientVisits objPatientVisit = new PatientVisits();
            objPatientVisit.PatientID = PatientID;
            objPatientVisit.ProviderID = ProviderID;
            objPatientVisit.VisitDate = DateTime.Now;
            objPatientVisit.VisitTime = DateTime.Now.ToShortTimeString();
            objPatientVisit.VisitCreatedOn = DateTime.Now;
            objPatientVisit.VisitCounter = visitcount == "" ? 1 :( Convert.ToInt32(visitcount)+1);
            objDBContext.PatientVisitsList.Add(objPatientVisit);

            objDBContext.SaveChanges();
            return  "Success";

        }

        public JsonResult DeletePatientInfo(Patient objPatient)
        {
            Patient objEditPatient = (from p in objDBContext.Patients
                                      where p.PatientID == objPatient.PatientID
                                      where p.PracticeID == objPatient.PracticeID
                                      select p).FirstOrDefault();

            string meg = "";
            try
            {  
                objEditPatient.StatusID = 2;
                objDBContext.SaveChanges();
                meg = "Success";
            }
            catch (Exception ex)
            {
                meg = "Failed";
                meg = ex.Message;
            }
            return Json(new { result = meg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientInfo(Patient objPatient)
        {
            Patient objGetPatient = (from p in objDBContext.Patients
                                      where p.PatientID == objPatient.PatientID
                                      where p.PracticeID == objPatient.PracticeID                                      
                                      select p).FirstOrDefault();

            return Json(objGetPatient, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPatientInfo(int PracticeID)
        {
            var objGetPatient = (from p in objDBContext.Patients                                     
                                     where p.PracticeID == PracticeID && p.StatusID ==1
                                     select p).ToList();
            return Json(objGetPatient, JsonRequestBehavior.AllowGet);
        }

        /////////////////////////////////////////////////////////////////////////
        ////////////PATIENT ENCOUNTERS///////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////

        public JsonResult CreatePatientNewEncounter(PatientEncounters objPEncounters)
        {
            string msg = "";
            MessageCls objMsgClass = new MessageCls();
            try
            {
                PatientEncounters objNewEncounter = new PatientEncounters
                {
                    //EncounterID =objPEncounters.EncounterID,
                    PatientID = objPEncounters.PatientID,
                    EquipID = objPEncounters.EquipID,
                    Date = objPEncounters.Date,
                    Notes = objPEncounters.Notes,
                    Equip_Options = objPEncounters.Equip_Options,
                    Presc_Physician = objPEncounters.Presc_Physician,
                    Delivery_Method = objPEncounters.Delivery_Method,
                    Start_Refill_Date = objPEncounters.Start_Refill_Date,
                    Equip_Inspected_By = objPEncounters.Equip_Inspected_By,
                    Equip_Deliv_Date = objPEncounters.Equip_Deliv_Date,
                    Facility_Name = objPEncounters.Facility_Name,
                    Facility_Address = objPEncounters.Facility_Address,
                    Diagnosis_Codes = objPEncounters.Diagnosis_Codes,
                    Est_Treatment_Dur = objPEncounters.Est_Treatment_Dur,
                    Equip_Serial_Num = objPEncounters.Equip_Serial_Num,
                    Type_Of_Equip = objPEncounters.Type_Of_Equip,
                    Drug = objPEncounters.Drug,
                    HCPCS_Code = objPEncounters.HCPCS_Code,
                    J_Code = objPEncounters.J_Code,
                    Po_Patient_Sign = objPEncounters.Po_Patient_Sign,
                    Po_Patient_Sign_Date = objPEncounters.Po_Patient_Sign_Date,
                    Po_CompanyRep_Sign = objPEncounters.Po_CompanyRep_Sign,
                    Po_Company_Rep_Sign_Date = objPEncounters.Po_Company_Rep_Sign_Date,
                    Po_Equip_Received_Date = objPEncounters.Po_Equip_Received_Date,
                    Mcr_Beneficiary_Sign = objPEncounters.Mcr_Beneficiary_Sign,
                    Mcr_Beneficiary_Sign_Date = objPEncounters.Mcr_Beneficiary_Sign_Date,
                    Mcr_Beneficiary_Name = objPEncounters.Mcr_Beneficiary_Name,
                    Mcr_Notes = objPEncounters.Mcr_Notes,
                    Pdr_Patient_Sign = objPEncounters.Pdr_Patient_Sign,
                    Pdr_Patient_Sign_Date = objPEncounters.Pdr_Patient_Sign_Date,
                    Pdr_Legalguardian_Sign = objPEncounters.Pdr_Legalguardian_Sign,
                    Pdr_Legalguardian_Name = objPEncounters.Pdr_Legalguardian_Name,
                    Pii_Patient_Sign = objPEncounters.Pii_Patient_Sign,
                    Pii_Patient_Sign_Date = objPEncounters.Pii_Patient_Sign_Date,
                    Pii_Legalguardian_Sign = objPEncounters.Pii_Legalguardian_Sign,
                    Pii_Reason_PatientUnsign = objPEncounters.Pii_Reason_PatientUnsign,
                    Pii_Guardian_Relation = objPEncounters.Pii_Guardian_Relation,
                    Pii_Guardian_Firstname = objPEncounters.Pii_Guardian_Firstname,
                    Pii_Guardian_Lastname = objPEncounters.Pii_Guardian_Lastname,
                    Pii_Guardian_Address1 = objPEncounters.Pii_Guardian_Address1,
                    Pii_Guardian_Address2 = objPEncounters.Pii_Guardian_Address2,
                    Pii_Guardian_City = objPEncounters.Pii_Guardian_City,
                    Pii_Guardian_State = objPEncounters.Pii_Guardian_State,
                    Pii_Guardian_Zip = objPEncounters.Pii_Guardian_Zip,
                    Pii_Guardian_Email = objPEncounters.Pii_Guardian_Email,
                    Pii_Guardian_Phone = objPEncounters.Pii_Guardian_Phone,
                    Ptn_Physician_Sign = objPEncounters.Ptn_Physician_Sign,
                    Ptn_Physician_Sign_Date = objPEncounters.Ptn_Physician_Sign_Date,
                    Ptn_Physician_Name = objPEncounters.Ptn_Physician_Name,
                    Ptn_Intravenous_Conti_Times = objPEncounters.Ptn_Intravenous_Conti_Times,
                    Ptn_Intravenous_Conti_Days = objPEncounters.Ptn_Intravenous_Conti_Days,
                    Ptn_Continu_Administrat = objPEncounters.Ptn_Continu_Administrat,
                    Ptn_Continu_Adminstrat_IFno = objPEncounters.Ptn_Continu_Adminstrat_IFno,
                    Ptn_Intravenous_Infusion = objPEncounters.Ptn_Intravenous_Infusion,
                    Ptn_Presc_Of_Equip = objPEncounters.Ptn_Presc_Of_Equip,
                    Dmeif_Supplier_Sign = objPEncounters.Dmeif_Supplier_Sign,
                    Dmeif_Supplier_Sign_Date = objPEncounters.Dmeif_Supplier_Sign_Date,
                    Dmeif_Initial_Date = objPEncounters.Dmeif_Initial_Date,
                    Dmeif_Revised_Date = objPEncounters.Dmeif_Revised_Date,
                    Dmeif_Recertification_Date = objPEncounters.Dmeif_Recertification_Date,
                    StatusID = 1
                };
                objDBContext.PatientEncountersList.Add(objNewEncounter);
                objDBContext.SaveChanges();


                objMsgClass.ID = objNewEncounter.EncounterID;
                objMsgClass.Status = "Success";

                return Json(objMsgClass, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                msg = "Failed";
                msg = ex.Message;

                objMsgClass.ID = 0;
                objMsgClass.Status = ex.Message;
            }
            return Json(objMsgClass, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CopyEncounter(int PatientID, int EncounterID)
        {
            string msg = "";
            try
            {
                PatientEncounters objPEncounters = (from p in objDBContext.PatientEncountersList
                                                     where p.PatientID == PatientID
                                                     where p.EncounterID == EncounterID
                                                     select p).FirstOrDefault();

                PatientEncounters objCpyEncounter = new PatientEncounters
                {
                    //EncounterID =objPEncounters.EncounterID,
                    PatientID = objPEncounters.PatientID,
                    EquipID = objPEncounters.EquipID,
                    Date = objPEncounters.Date,
                    Notes = objPEncounters.Notes,
                    Equip_Options = objPEncounters.Equip_Options,
                    Presc_Physician = objPEncounters.Presc_Physician,
                    Delivery_Method = objPEncounters.Delivery_Method,
                    Start_Refill_Date = objPEncounters.Start_Refill_Date,
                    Equip_Inspected_By = objPEncounters.Equip_Inspected_By,
                    Equip_Deliv_Date = objPEncounters.Equip_Deliv_Date,
                    Facility_Name = objPEncounters.Facility_Name,
                    Facility_Address = objPEncounters.Facility_Address,
                    Diagnosis_Codes = objPEncounters.Diagnosis_Codes,
                    Est_Treatment_Dur = objPEncounters.Est_Treatment_Dur,
                    Equip_Serial_Num = objPEncounters.Equip_Serial_Num,
                    Type_Of_Equip = objPEncounters.Type_Of_Equip,
                    Drug = objPEncounters.Drug,
                    HCPCS_Code = objPEncounters.HCPCS_Code,
                    J_Code = objPEncounters.J_Code,
                    Po_Patient_Sign = objPEncounters.Po_Patient_Sign,
                    Po_Patient_Sign_Date = objPEncounters.Po_Patient_Sign_Date,
                    Po_CompanyRep_Sign = objPEncounters.Po_CompanyRep_Sign,
                    Po_Company_Rep_Sign_Date = objPEncounters.Po_Company_Rep_Sign_Date,
                    Po_Equip_Received_Date = objPEncounters.Po_Equip_Received_Date,
                    Mcr_Beneficiary_Sign = objPEncounters.Mcr_Beneficiary_Sign,
                    Mcr_Beneficiary_Sign_Date = objPEncounters.Mcr_Beneficiary_Sign_Date,
                    Mcr_Beneficiary_Name = objPEncounters.Mcr_Beneficiary_Name,
                    Mcr_Notes = objPEncounters.Mcr_Notes,
                    Pdr_Patient_Sign = objPEncounters.Pdr_Patient_Sign,
                    Pdr_Patient_Sign_Date = objPEncounters.Pdr_Patient_Sign_Date,
                    Pdr_Legalguardian_Sign = objPEncounters.Pdr_Legalguardian_Sign,
                    Pdr_Legalguardian_Name = objPEncounters.Pdr_Legalguardian_Name,
                    Pii_Patient_Sign = objPEncounters.Pii_Patient_Sign,
                    Pii_Patient_Sign_Date = objPEncounters.Pii_Patient_Sign_Date,
                    Pii_Legalguardian_Sign = objPEncounters.Pii_Legalguardian_Sign,
                    Pii_Reason_PatientUnsign = objPEncounters.Pii_Reason_PatientUnsign,
                    Pii_Guardian_Relation = objPEncounters.Pii_Guardian_Relation,
                    Pii_Guardian_Firstname = objPEncounters.Pii_Guardian_Firstname,
                    Pii_Guardian_Lastname = objPEncounters.Pii_Guardian_Lastname,
                    Pii_Guardian_Address1 = objPEncounters.Pii_Guardian_Address1,
                    Pii_Guardian_Address2 = objPEncounters.Pii_Guardian_Address2,
                    Pii_Guardian_City = objPEncounters.Pii_Guardian_City,
                    Pii_Guardian_State = objPEncounters.Pii_Guardian_State,
                    Pii_Guardian_Zip = objPEncounters.Pii_Guardian_Zip,
                    Pii_Guardian_Email = objPEncounters.Pii_Guardian_Email,
                    Pii_Guardian_Phone = objPEncounters.Pii_Guardian_Phone,
                    Ptn_Physician_Sign = objPEncounters.Ptn_Physician_Sign,
                    Ptn_Physician_Sign_Date = objPEncounters.Ptn_Physician_Sign_Date,
                    Ptn_Physician_Name = objPEncounters.Ptn_Physician_Name,
                    Ptn_Intravenous_Conti_Times = objPEncounters.Ptn_Intravenous_Conti_Times,
                    Ptn_Intravenous_Conti_Days = objPEncounters.Ptn_Intravenous_Conti_Days,
                    Ptn_Continu_Administrat = objPEncounters.Ptn_Continu_Administrat,
                    Ptn_Continu_Adminstrat_IFno = objPEncounters.Ptn_Continu_Adminstrat_IFno,
                    Ptn_Intravenous_Infusion = objPEncounters.Ptn_Intravenous_Infusion,
                    Ptn_Presc_Of_Equip = objPEncounters.Ptn_Presc_Of_Equip,
                    Dmeif_Supplier_Sign = objPEncounters.Dmeif_Supplier_Sign,
                    Dmeif_Supplier_Sign_Date = objPEncounters.Dmeif_Supplier_Sign_Date,
                    Dmeif_Initial_Date = objPEncounters.Dmeif_Initial_Date,
                    Dmeif_Revised_Date = objPEncounters.Dmeif_Revised_Date,
                    Dmeif_Recertification_Date = objPEncounters.Dmeif_Recertification_Date,
                    StatusID = 1
                };
                objDBContext.PatientEncountersList.Add(objCpyEncounter);
                objDBContext.SaveChanges();

                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = "Failed";
                msg = ex.Message;
            }


            return Json(new { result = msg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditPatientEncounter(PatientEncounters objPEncounters)
        {
            string msg = "";
            try
            {
                PatientEncounters objNewEncounter = (from p in objDBContext.PatientEncountersList
                                                     where p.PatientID == objPEncounters.PatientID
                                                     where p.EncounterID == objPEncounters.EncounterID
                                                     select p).FirstOrDefault();

                //EncounterID =objPEncounters.EncounterID,
                // objNewEncounter. PatientID = objPEncounters.PatientID;
                objNewEncounter.EquipID = objPEncounters.EquipID;
                objNewEncounter.Date = objPEncounters.Date;
                objNewEncounter.Notes = objPEncounters.Notes;
                objNewEncounter.Equip_Options = objPEncounters.Equip_Options;
                objNewEncounter.Presc_Physician = objPEncounters.Presc_Physician;
                objNewEncounter.Delivery_Method = objPEncounters.Delivery_Method;
                objNewEncounter.Start_Refill_Date = objPEncounters.Start_Refill_Date;
                objNewEncounter.Equip_Inspected_By = objPEncounters.Equip_Inspected_By;
                objNewEncounter.Equip_Deliv_Date = objPEncounters.Equip_Deliv_Date;
                objNewEncounter.Facility_Name = objPEncounters.Facility_Name;
                objNewEncounter.Facility_Address = objPEncounters.Facility_Address;
                objNewEncounter.Diagnosis_Codes = objPEncounters.Diagnosis_Codes;
                objNewEncounter.Est_Treatment_Dur = objPEncounters.Est_Treatment_Dur;
                objNewEncounter.Equip_Serial_Num = objPEncounters.Equip_Serial_Num;
                objNewEncounter.Type_Of_Equip = objPEncounters.Type_Of_Equip;
                objNewEncounter.Drug = objPEncounters.Drug;
                objNewEncounter.HCPCS_Code = objPEncounters.HCPCS_Code;
                objNewEncounter.J_Code = objPEncounters.J_Code;
                objNewEncounter.Po_Patient_Sign = objPEncounters.Po_Patient_Sign;
                objNewEncounter.Po_Patient_Sign_Date = objPEncounters.Po_Patient_Sign_Date;
                objNewEncounter.Po_CompanyRep_Sign = objPEncounters.Po_CompanyRep_Sign;
                objNewEncounter.Po_Company_Rep_Sign_Date = objPEncounters.Po_Company_Rep_Sign_Date;
                objNewEncounter.Po_Equip_Received_Date = objPEncounters.Po_Equip_Received_Date;
                objNewEncounter.Mcr_Beneficiary_Sign = objPEncounters.Mcr_Beneficiary_Sign;
                objNewEncounter.Mcr_Beneficiary_Sign_Date = objPEncounters.Mcr_Beneficiary_Sign_Date;
                objNewEncounter.Mcr_Beneficiary_Name = objPEncounters.Mcr_Beneficiary_Name;
                objNewEncounter.Mcr_Notes = objPEncounters.Mcr_Notes;
                objNewEncounter.Pdr_Patient_Sign = objPEncounters.Pdr_Patient_Sign;
                objNewEncounter.Pdr_Patient_Sign_Date = objPEncounters.Pdr_Patient_Sign_Date;
                objNewEncounter.Pdr_Legalguardian_Sign = objPEncounters.Pdr_Legalguardian_Sign;
                objNewEncounter.Pdr_Legalguardian_Name = objPEncounters.Pdr_Legalguardian_Name;
                objNewEncounter.Pii_Patient_Sign = objPEncounters.Pii_Patient_Sign;
                objNewEncounter.Pii_Patient_Sign_Date = objPEncounters.Pii_Patient_Sign_Date;
                objNewEncounter.Pii_Legalguardian_Sign = objPEncounters.Pii_Legalguardian_Sign;
                objNewEncounter.Pii_Reason_PatientUnsign = objPEncounters.Pii_Reason_PatientUnsign;
                objNewEncounter.Pii_Guardian_Relation = objPEncounters.Pii_Guardian_Relation;
                objNewEncounter.Pii_Guardian_Firstname = objPEncounters.Pii_Guardian_Firstname;
                objNewEncounter.Pii_Guardian_Lastname = objPEncounters.Pii_Guardian_Lastname;
                objNewEncounter.Pii_Guardian_Address1 = objPEncounters.Pii_Guardian_Address1;
                objNewEncounter.Pii_Guardian_Address2 = objPEncounters.Pii_Guardian_Address2;
                objNewEncounter.Pii_Guardian_City = objPEncounters.Pii_Guardian_City;
                objNewEncounter.Pii_Guardian_State = objPEncounters.Pii_Guardian_State;
                objNewEncounter.Pii_Guardian_Zip = objPEncounters.Pii_Guardian_Zip;
                objNewEncounter.Pii_Guardian_Email = objPEncounters.Pii_Guardian_Email;
                objNewEncounter.Pii_Guardian_Phone = objPEncounters.Pii_Guardian_Phone;
                objNewEncounter.Ptn_Physician_Sign = objPEncounters.Ptn_Physician_Sign;
                objNewEncounter.Ptn_Physician_Sign_Date = objPEncounters.Ptn_Physician_Sign_Date;
                objNewEncounter.Ptn_Physician_Name = objPEncounters.Ptn_Physician_Name;
                objNewEncounter.Ptn_Intravenous_Conti_Times = objPEncounters.Ptn_Intravenous_Conti_Times;
                objNewEncounter.Ptn_Intravenous_Conti_Days = objPEncounters.Ptn_Intravenous_Conti_Days;
                objNewEncounter.Ptn_Continu_Administrat = objPEncounters.Ptn_Continu_Administrat;
                objNewEncounter.Ptn_Continu_Adminstrat_IFno = objPEncounters.Ptn_Continu_Adminstrat_IFno;
                objNewEncounter.Ptn_Intravenous_Infusion = objPEncounters.Ptn_Intravenous_Infusion;
                objNewEncounter.Ptn_Presc_Of_Equip = objPEncounters.Ptn_Presc_Of_Equip;
                objNewEncounter.Dmeif_Supplier_Sign = objPEncounters.Dmeif_Supplier_Sign;
                objNewEncounter.Dmeif_Supplier_Sign_Date = objPEncounters.Dmeif_Supplier_Sign_Date;
                objNewEncounter.Dmeif_Initial_Date = objPEncounters.Dmeif_Initial_Date;
                objNewEncounter.Dmeif_Revised_Date = objPEncounters.Dmeif_Revised_Date;
                objNewEncounter.Dmeif_Recertification_Date = objPEncounters.Dmeif_Recertification_Date;
                objNewEncounter.StatusID = 1;
                objDBContext.SaveChanges();
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = "Failed";
                msg = ex.Message;
            }


            return Json(new { result = msg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPatientEncounters(Patient objPatient)
        {
            var objPEncounters = (from p in objDBContext.PatientEncountersList
                                                where p.PatientID == objPatient.PatientID
                                                where p.StatusID != 2
                                  select new
                                  {
                                      p.EncounterID,
                                      p.PatientID,
                                      p.EquipID,
                                      p.Date,
                                      p.Notes,
                                      p.Equip_Options,
                                      p.Presc_Physician,
                                      p.Delivery_Method,
                                      p.Start_Refill_Date,
                                      p.Equip_Inspected_By,
                                      p.Equip_Deliv_Date,
                                      p.Facility_Name,
                                      p.Facility_Address,
                                      p.Diagnosis_Codes,
                                      p.Est_Treatment_Dur,
                                      p.Equip_Serial_Num,
                                      p.Type_Of_Equip,
                                      p.Drug,
                                      p.HCPCS_Code,
                                      p.J_Code,
                                      p.Po_Patient_Sign,
                                      p.Po_Patient_Sign_Date,
                                      p.Po_CompanyRep_Sign,
                                      p.Po_Company_Rep_Sign_Date,
                                      p.Po_Equip_Received_Date,
                                      p.Mcr_Beneficiary_Sign,
                                      p.Mcr_Beneficiary_Sign_Date,
                                      p.Mcr_Beneficiary_Name,
                                      p.Mcr_Notes,
                                      p.Pdr_Patient_Sign,
                                      p.Pdr_Patient_Sign_Date,
                                      p.Pdr_Legalguardian_Sign,
                                      p.Pdr_Legalguardian_Name,
                                      p.Pii_Patient_Sign,
                                      p.Pii_Patient_Sign_Date,
                                      p.Pii_Legalguardian_Sign,
                                      p.Pii_Reason_PatientUnsign,
                                      p.Pii_Guardian_Relation,
                                      p.Pii_Guardian_Firstname,
                                      p.Pii_Guardian_Lastname,
                                      p.Pii_Guardian_Address1,
                                      p.Pii_Guardian_Address2,
                                      p.Pii_Guardian_City,
                                      p.Pii_Guardian_State,
                                      p.Pii_Guardian_Zip,
                                      p.Pii_Guardian_Email,
                                      p.Pii_Guardian_Phone,
                                      p.Ptn_Physician_Sign,
                                      p.Ptn_Physician_Sign_Date,
                                      p.Ptn_Physician_Name,
                                      p.Ptn_Intravenous_Conti_Times,
                                      p.Ptn_Intravenous_Conti_Days,
                                      p.Ptn_Continu_Administrat,
                                      p.Ptn_Continu_Adminstrat_IFno,
                                      p.Ptn_Intravenous_Infusion,
                                      p.Ptn_Presc_Of_Equip,
                                      p.Dmeif_Supplier_Sign,
                                      p.Dmeif_Supplier_Sign_Date,
                                      p.Dmeif_Initial_Date,
                                      p.Dmeif_Revised_Date,
                                      p.Dmeif_Recertification_Date,
                                      p.StatusID
                                  }).ToList();

            return Json(objPEncounters, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientSingleEncounter(PatientEncounters objPEncounters)
        {
            var objPEncounter = (from p in objDBContext.PatientEncountersList
                                               where p.PatientID == objPEncounters.PatientID
                                               where p.EncounterID == objPEncounters.EncounterID
                                               where p.StatusID != 2
                                               select new
                                               {p.EncounterID,
                                                   p.PatientID,
                                                   p.EquipID,
                                                   p.Date,
                                                   p.Notes,
                                                   p.Equip_Options,
                                                   p.Presc_Physician,
                                                   p.Delivery_Method,
                                                   p.Start_Refill_Date,
                                                   p.Equip_Inspected_By,
                                                   p.Equip_Deliv_Date,
                                                   p.Facility_Name,
                                                   p.Facility_Address,
                                                   p.Diagnosis_Codes,
                                                   p.Est_Treatment_Dur,
                                                   p.Equip_Serial_Num,
                                                   p.Type_Of_Equip,
                                                   p.Drug,
                                                   p.HCPCS_Code,
                                                   p.J_Code,
                                                   p.Po_Patient_Sign,
                                                   p.Po_Patient_Sign_Date,
                                                   p.Po_CompanyRep_Sign,
                                                   p.Po_Company_Rep_Sign_Date,
                                                   p.Po_Equip_Received_Date,
                                                   p.Mcr_Beneficiary_Sign,
                                                   p.Mcr_Beneficiary_Sign_Date,
                                                   p.Mcr_Beneficiary_Name,
                                                   p.Mcr_Notes,
                                                   p.Pdr_Patient_Sign,
                                                   p.Pdr_Patient_Sign_Date,
                                                   p.Pdr_Legalguardian_Sign,
                                                   p.Pdr_Legalguardian_Name,
                                                   p.Pii_Patient_Sign,
                                                   p.Pii_Patient_Sign_Date,
                                                   p.Pii_Legalguardian_Sign,
                                                   p.Pii_Reason_PatientUnsign,
                                                   p.Pii_Guardian_Relation,
                                                   p.Pii_Guardian_Firstname,
                                                   p.Pii_Guardian_Lastname,
                                                   p.Pii_Guardian_Address1,
                                                   p.Pii_Guardian_Address2,
                                                   p.Pii_Guardian_City,
                                                   p.Pii_Guardian_State,
                                                   p.Pii_Guardian_Zip,
                                                   p.Pii_Guardian_Email,
                                                   p.Pii_Guardian_Phone,
                                                   p.Ptn_Physician_Sign,
                                                   p.Ptn_Physician_Sign_Date,
                                                   p.Ptn_Physician_Name,
                                                   p.Ptn_Intravenous_Conti_Times,
                                                   p.Ptn_Intravenous_Conti_Days,
                                                   p.Ptn_Continu_Administrat,
                                                   p.Ptn_Continu_Adminstrat_IFno,
                                                   p.Ptn_Intravenous_Infusion,
                                                   p.Ptn_Presc_Of_Equip,
                                                   p.Dmeif_Supplier_Sign,
                                                   p.Dmeif_Supplier_Sign_Date,
                                                   p.Dmeif_Initial_Date,
                                                   p.Dmeif_Revised_Date,
                                                   p.Dmeif_Recertification_Date,
                                                   p.StatusID
                                               }).FirstOrDefault();

            return Json(objPEncounter, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePatientEncounter(PatientEncounters objPEncounters)
        {
            string msg = "";
            try
            {
                PatientEncounters objPEncounter = (from p in objDBContext.PatientEncountersList
                                                   where p.PatientID == objPEncounters.PatientID
                                                   where p.EncounterID == objPEncounters.EncounterID
                                                   where p.StatusID == 1
                                                   select p).FirstOrDefault();

                objPEncounter.StatusID = 2;
                objDBContext.SaveChanges();
                msg = "Success";
            }
            catch(Exception ex)
            {
                msg = "Failed";
                msg = ex.Message;
            }
            return Json(new { result = msg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientEncounterAccessories(int EncounterID)
        {
            IEnumerable<vwPdfGenEnconterAccess> vwpdfAcc1 = (from v in objDBContext.vwPDFGenEncAcc
                                                             where v.EncounterID == EncounterID                                                             
                                                             select v).ToList();

            return Json(vwpdfAcc1, JsonRequestBehavior.AllowGet);
        }


        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        public JsonResult GetPracticeEquipments(Equipment objEquip)
        {
            var objParcticeEquipments = (from p in objDBContext.Equipments
                                         where p.PracticeID == objEquip.PracticeID
                                         select p).ToList();
            return Json(objParcticeEquipments, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetEquipmentAccessories(Equipment objEquip)
        {
            var objEquipAccessories = (from p in objDBContext.vwEquipmentAccessoriesList
                                       where p.EquipID == objEquip.EquipID
                                       select p).ToList();

            return Json(objEquipAccessories,JsonRequestBehavior.AllowGet); 
        }
        
        public JsonResult CreateEncounterAccessories(List<Encounter_Accessories> objEncounterAccs)
        {
            string msg = "";
            try {
              

                foreach (Encounter_Accessories objItem in objEncounterAccs)
                {
                    var objDelEncAcces = (from p in objDBContext.Encounter_AccessoriesList
                                          where p.EncounterID == objItem.EncounterID
                                          select p).FirstOrDefault();

                    objDBContext.Encounter_AccessoriesList.Remove(objDelEncAcces);
                    objDBContext.SaveChanges();
                }              


                foreach (Encounter_Accessories objItem in objEncounterAccs)
                {
                    Encounter_Accessories objNewEnAcces = new Encounter_Accessories();
                    objNewEnAcces.EncounterID = objItem.EncounterID;
                    objNewEnAcces.AccessoryID = objItem.AccessoryID;

                    objDBContext.Encounter_AccessoriesList.Add(objNewEnAcces);
                    objDBContext.SaveChanges();
                }

                msg = "Success";
            }
            catch(Exception ex)
            {
                msg = "Failed";
                msg = ex.Message;
            }

            return Json(new { result = msg }, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetSearchPatients(vwSearchPatients objView)
        {
            try {

                var objSearchPatients = (from p in objDBContext.vwSearch
                                         where p.PracticeID == objView.PracticeID
                                         select p).ToList();

                if (objView.FirstName != null && objView.FirstName != "")
                    objSearchPatients = objSearchPatients.Where(p => p.FirstName == objView.FirstName).ToList();
                if (objView.LastName != null && objView.LastName != "")
                    objSearchPatients = objSearchPatients.Where(p => p.LastName == objView.LastName).ToList();
                if (objView.PhoneNumber1 != null && objView.PhoneNumber1 != "")
                    objSearchPatients = objSearchPatients.Where(p => p.PhoneNumber1 == objView.PhoneNumber1).ToList();
                if (objView.ProviderID != 0 && objView.ProviderID != null)
                    objSearchPatients = objSearchPatients.Where(p => p.ProviderID == objView.ProviderID).ToList();
                if (objView.InsuranceName != null &&  objView.InsuranceName != "")
                    objSearchPatients = objSearchPatients.Where(p => p.InsuranceName == objView.InsuranceName).ToList();               
                if (objView.Date_Of_Birth != null)
                     objSearchPatients = objSearchPatients.Where(p => p.Date_Of_Birth == objView.Date_Of_Birth).ToList();                            
                if (objView.FromDate != null)
                     objSearchPatients = objSearchPatients.Where(p => p.FromDate >= objView.FromDate).ToList();
                if (objView.ToDate != null)
                    objSearchPatients = objSearchPatients.Where(p => p.ToDate >= objView.FromDate).ToList();

                if (objSearchPatients != null)
                {
                    return Json(objSearchPatients, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "No Data found" }, JsonRequestBehavior.AllowGet);
                }

                //return Json(objView, JsonRequestBehavior.AllowGet);
               
            }
            catch(Exception ex)
            {
                return Json(new { result = ex.Message }, JsonRequestBehavior.AllowGet);
            }          
           
        }

        public JsonResult GetAlerts(vwPatient_PatientEncounters patient)
        {
            var objAlerts = (from p in objDBContext.vwPatients
                             where p.PracticeID == patient.PracticeID && (p.StatusID == 3 || p.StatusID == 4)
                             select p).ToList();

            return Json(objAlerts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPracticeProviders(int PracticeID)
        {
           var model = (from p in objDBContext.PracticeUsers
                        where p.StatusID == 1 && p.PracticeID == PracticeID && p.PracticeUserType == "Provider"
                                               select p).ToList();
           return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPractices()
        {
            var model = (from p in objDBContext.Practices
                         where p.StatusID == 1
                         select p.PracticeName).ToList();
            if (model != null)
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { resul="No Data found"}, JsonRequestBehavior.AllowGet);
            }
        }


        ////////////////////////////////////////////////////////////////////////////////
        ///////////////////MESSAGES SERVICES../////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        public JsonResult PostMessage(int EncounterID, int CreatedUserID, string MessageDescription)
        {
            string meg = "";
            try
            {
                Encounter_Messages enc = new Encounter_Messages();
                enc.Message_Desc = MessageDescription;
                enc.Created_Date = DateTime.Now;
                enc.Encounter_ID = EncounterID;
                int UserID = CreatedUserID;
                enc.Posted_By = UserID;
                enc.Status_ID = 8;
                enc.Modified_Date = null;
                objDBContext.Enc_Msg.Add(enc);
                objDBContext.SaveChanges();                
                meg = "Success";
            }
            catch (Exception ex)
            {
                meg = "Failed";
                meg = ex.Message;
            }
            return Json(new { result = meg }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetBillerMessages(int PracticeID)
        {
            var BillerList = (from p in objDBContext.vwBillerMessagesList
                              where p.PracticeUserType == "Biller"
                              select p).ToList();

            if (BillerList != null)
            {
                return Json(BillerList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { resul = "No Data found" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTodaySchedules()
        {
            var TodaySchedulesList = (from p in objDBContext.vwTodayScheduleList     
                                     select p).ToList();
            if (TodaySchedulesList != null)
            {
                return Json(TodaySchedulesList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "No Data found" }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
