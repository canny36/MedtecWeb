using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;
using System.Collections;
using ReportManagement;
using System.Text;
using System.IO;
using System.Drawing;


namespace MedtecMedical_App.Controllers
{
    public class PatientInfoController : PdfViewController
    {
        private MedtecContext db = new MedtecContext();
        public int ENCid;
        //
        // GET: /PatientInfo/

        public ViewResult Index()
        {
            return View(db.Patients.ToList());
        }

        //
        // GET: /PatientInfo/Details/5

        public ViewResult Details(int id)
        {
            Patient patient = db.Patients.Find(id);
            return View(patient);
        }

        //
        // GET: /PatientInfo/Create

        public ActionResult Patients()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            //IEnumerable<Patient> model = (from p in db.Patients
            //                              where p.StatusID != 2 && p.PracticeID == PracticeID
            //                              select p).ToList();
            //IEnumerable<vwSearchPatients> model;
            //if (vwsearch == null)
            //{
            //    model = (from p in db.vwSearch
            //             select p).ToList();
            //}
            //else
            //{
            //    model = vwsearch;
            //}
            ViewBag.ProviderID = from v in db.PracticeUsers
                                 where v.StatusID != 2 && v.PracticeID == PracticeID && v.PracticeUserType == "Provider"
                                 select v; 
            return View();
        }

        //public ActionResult Patients(vwSearchPatients vwsearch)
        //{
        //    int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
        //    //IEnumerable<Patient> model = (from p in db.Patients
        //    //                              where p.StatusID != 2 && p.PracticeID == PracticeID
        //    //                              select p).ToList();

          
        //    ViewBag.ProviderID = from v in db.PracticeUsers
        //                         where v.StatusID != 2 && v.PracticeID == PracticeID && v.PracticeUserType == "Provider"
        //                         select v;
        //    return View(vwsearch);
        //}

        public ActionResult Edit(int id,int flag)
        {
            //var patientID = (from v in db.PatientEncountersList
            //                where v.EncounterID == id
            //                select v.PatientID);
            IEnumerable<PatientEncounters> model = (from p in db.PatientEncountersList
                                                    where p.StatusID != 2 && p.PatientID == id
                                                    select p).ToList();

            db.PatientEncountersList.Find(id);
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            ViewBag.ProviderID = from v in db.PracticeUsers
                                 where v.StatusID != 2 && v.PracticeID == PracticeID && v.PracticeUserType == "Provider"
                                 select v; 
            PatientEncounters pat = new PatientEncounters();
            if (model.Count() == 0)
            {
                List<PatientEncounters> patient = new List<PatientEncounters>();
                pat.Equip_Options = pat.Presc_Physician = pat.Delivery_Method = pat.Equip_Inspected_By = pat.Facility_Name = "";
                pat.patnt = db.Patients.Find(id);
                patient.Add(pat);
                return View(patient);
            }
            else
                return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PatientEncounters patient)
        {

            db.Entry(patient.patnt).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit/" + patient.patnt.PatientID);
        }

        public ActionResult EditPatientEncounters(int id)
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            IEnumerable<Encounter_Accessories> patientenc = (from p in db.Encounter_AccessoriesList
                                                             where p.EncounterID == id && p.patientenc.equip.PracticeID == PracticeID
                                                             select p).ToList();
            if (patientenc.Count() > 0)
            {
                if (patientenc.First().patientenc.StatusID == 1)
                    ViewBag.StatusID = from v in db.StatusList
                                       where v.StatusID == 1 || v.StatusID == 5
                                       select v;
                else
                    ViewBag.StatusID = from v in db.StatusList
                                       where v.StatusID == 4 || v.StatusID == 5
                                       select v;
                if (patientenc.First().patientenc.Delivery_Method == "")
                    patientenc.First().patientenc.Delivery_Method = "to nursing facility";
                var list = new SelectList(new[]
                                          {
                                              new {ID="directly to beneficiary",Name="directly to beneficiary"},
                                              new{ID="shipping service",Name="shipping service"},
                                              new{ID="to nursing facility",Name="to nursing facility"},
                                          },
                        "ID", "Name", patientenc.First().patientenc.Delivery_Method);

                ViewData["list"] = list;


                var listPrescEquip = new SelectList(new[]
                                          {
                                              new {ID="Ambit",Name="Ambit"},
                                              new{ID="CADD",Name="CADD"},
                                              new{ID="Walkmed",Name="Walkmed"},
                                                 new{ID="Curlin",Name="Curlin"}
                                          },
                              "ID", "Name", patientenc.First().patientenc.Ptn_Presc_Of_Equip);

                ViewData["listPrescEquip"] = listPrescEquip;

                if (patientenc.First().patientenc.Ptn_Continu_Administrat == null)
                    patientenc.First().patientenc.Ptn_Continu_Administrat = false;
                if (patientenc.First().patientenc.Ptn_Intravenous_Infusion == null)
                    patientenc.First().patientenc.Ptn_Intravenous_Infusion = false;
            }
         
            ViewBag.EquipID = from v in db.Equipments
                              where v.StatusID != 2 && v.PracticeID == PracticeID
                              select v;
         
        
            Encounter_Accessories enc = new Encounter_Accessories();
            if (patientenc.Count() == 0)
            {
                List<Encounter_Accessories> patient = new List<Encounter_Accessories>();
                enc.EncAccID = enc.EncounterID = enc.AccessoryID = enc.Quantity = 0;
                enc.patientenc = db.PatientEncountersList.Find(id);
                patient.Add(enc);
                if (patient.First().patientenc.StatusID == 1)
                    ViewBag.StatusID = from v in db.StatusList
                                       where v.StatusID == 1 || v.StatusID == 5
                                       select v;
                else
                    ViewBag.StatusID = from v in db.StatusList
                                       where v.StatusID == 4 || v.StatusID == 5
                                       select v;
                if (patient.First().patientenc.Delivery_Method == "")
                    patient.First().patientenc.Delivery_Method = "to nursing facility";
                var list = new SelectList(new[]
                                          {
                                              new {ID="directly to beneficiary",Name="directly to beneficiary"},
                                              new{ID="shipping service",Name="shipping service"},
                                              new{ID="to nursing facility",Name="to nursing facility"},
                                          },
                        "ID", "Name", patient.First().patientenc.Delivery_Method);

                ViewData["list"] = list;


                var listPrescEquip = new SelectList(new[]
                                          {
                                              new {ID="Ambit",Name="Ambit"},
                                              new{ID="CADD",Name="CADD"},
                                              new{ID="Walkmed",Name="Walkmed"},
                                                 new{ID="Curlin",Name="Curlin"}
                                          },
                              "ID", "Name", patient.First().patientenc.Ptn_Presc_Of_Equip);

                ViewData["listPrescEquip"] = listPrescEquip;

                if (patient.First().patientenc.Ptn_Continu_Administrat == null)
                    patient.First().patientenc.Ptn_Continu_Administrat = false;
                if (patient.First().patientenc.Ptn_Intravenous_Infusion == null)
                    patient.First().patientenc.Ptn_Intravenous_Infusion = false;

                return View(patient);
            }
            else
                return View(patientenc);

        }

        [HttpPost]
        public ActionResult EditPatientEncounters(Encounter_Accessories encounter, FormCollection frm)
        {
            var EquipID = frm["patientenc.EquipID"];
            string[] k = EquipID.Split(',');
            encounter.patientenc.EquipID = Convert.ToInt32(k[1]);
            var StatusID = frm["patientenc.StatusID"];
            encounter.patientenc.StatusID = Convert.ToInt32(StatusID);
            var Delivery_Method = frm["ddlUserType"];
            encounter.patientenc.Delivery_Method = Delivery_Method;
            var PresEquip = frm["ddlPrescEquip"];
            encounter.patientenc.Ptn_Presc_Of_Equip = PresEquip;
            //var ContAdm =Convert.ToBoolean(frm["patientenc.Ptn_Continu_Administrat"]);
            //encounter.patientenc.Ptn_Continu_Administrat =ContAdm;
            db.Entry(encounter.patientenc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("EditPatientEncounters/" + encounter.patientenc.EncounterID);
        }

        [HttpPost]
        public ActionResult DeleteEncounter_Accessory(int ID)
        {
            Encounter_Accessories enc = db.Encounter_AccessoriesList.Find(ID);
            db.Encounter_AccessoriesList.Remove(enc);
            db.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public ActionResult DeletePatient(int id)
        {
            Patient pnt = db.Patients.Find(id);
            pnt.StatusID = 2;
            db.SaveChanges();
            return Json(new { data = "Success" });

        }

        [HttpPost]
        public ActionResult DeletePatientEncounters(int EncounterID)
        {
            PatientEncounters pnt = db.PatientEncountersList.Find(EncounterID);
            pnt.StatusID = 2;
            db.SaveChanges();
            return Json(new { data = "Success" });
        }

        public ActionResult AddNewPatient()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            //ViewBag.ProviderID = new SelectList(from v in db.PracticeUsers
            //                     where v.StatusID != 2 && v.PracticeID == PracticeID && v.PracticeUserType == "Provider"
            //                                    select v, "UserID", "FirstName");
            ViewBag.ProviderID = from v in db.PracticeUsers
                                 where v.StatusID != 2 && v.PracticeID == PracticeID && v.PracticeUserType == "Provider"
                              select v;            
            return View();
        }
        [HttpPost]
        public ActionResult AddNewPatient(Patient pat, FormCollection frm)
        {
            var s = frm["ProviderID"];
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            pat.PracticeID = PracticeID;
            pat.StatusID = 1;
            db.Patients.Add(pat);
            db.SaveChanges();            
            return RedirectToAction("Patients"); 
        }

        private readonly HtmlViewRenderer htmlViewRenderer;

        public ActionResult PrintCustomers(int id)
        {
            ENCid = id;
            //var model=PreparePDF(id);
            string path = this.ViewPdf("", "GeneratedPDF", PreparePDF(id), id);
            return Json(path);
            //return RedirectToAction("EditPatientEncounters/" + id);
            //return View(model);
        }


        private vwPdfGenEncounterInfo PreparePDF(int id)
        {
            //    vwPDFGeneration std = db.vwPDFGen.Find(id);
            //    return std;
            ENCid = id;
            vwPdfGenEncounterInfo vwpdfGen1 = db.vwPDFGenEncInfo.Find(id);
            if (vwpdfGen1.Delivery_Method == "")
                vwpdfGen1.Delivery_Method = "to nursing facility";
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/PDF/") + vwpdfGen1.EncounterID);
            if (vwpdfGen1.Driver_Licence_Img != null)
            {
                byte[] buffer = vwpdfGen1.Driver_Licence_Img;
                SavePDFImages(buffer,vwpdfGen1.EncounterID , "Driver_Licence_Img");
            }
            if (vwpdfGen1.Po_Patient_Sign!= null)
            {
                byte[] buffer = vwpdfGen1.Po_Patient_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Po_Patient_Sign");
            }
            if (vwpdfGen1.Po_CompanyRep_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Po_CompanyRep_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Po_CompanyRep_Sign");
            }
            if (vwpdfGen1.Mcr_Beneficiary_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Mcr_Beneficiary_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Mcr_Beneficiary_Sign");
            }
            if (vwpdfGen1.Pii_Patient_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Pii_Patient_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Pii_Patient_Sign");
            }
            if (vwpdfGen1.Pii_Legalguardian_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Pii_Legalguardian_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Pii_Legalguardian_Sign");
            }
            if (vwpdfGen1.Ptn_Physician_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Ptn_Physician_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Ptn_Physician_Sign");
            }
            if (vwpdfGen1.Dmeif_Supplier_Sign != null)
            {
                byte[] buffer = vwpdfGen1.Dmeif_Supplier_Sign;
                SavePDFImages(buffer, vwpdfGen1.EncounterID, "Dmeif_Supplier_Sign");
            }
            var EquipID = from v in db.vwPDFGenEncInfo
                          where v.EncounterID == id
                          select v.EquipID;
            int Equip = EquipID.ToList()[0];
            //var vwpdfACc1 = from v in db.vwPDFGenEncAcc
            //                where v.EncounterID == id
            //                select v;
            IEnumerable<vwPdfGenEnconterAccess> vwpdfAcc1=(from v in db.vwPDFGenEncAcc
                                                  where v.EncounterID == id
                                                   select v).ToList();

            var vwEquip1 = (from v in db.EquipmentAccessorys
                           where v.EquipID == Equip
                           select v).ToList();

            foreach (var v in vwEquip1)
            {
                v.IsValid = false;
            }


            foreach (var v in vwEquip1)
            {
                foreach (var s in vwpdfAcc1)
                {
                    if (s.AccessoryID == v.AccessoryID)
                    {v.IsValid = true;
                    v.Quantity = s.Quantity;
                    }
                }
            }

            vwpdfGen1.EncAcc = vwEquip1.ToList();
            db.Dispose();
          
            //vwPdfGenEnconterAccess vwpdfEncAcc = db.vwPDFGenEncAcc.Find(id);
            return vwpdfGen1;

        }

      
      

         public ActionResult DeletePDFImages(int id)
         {
             if (System.IO.Directory.Exists(Server.MapPath("~/Images/PDF/" + id)))
                 System.IO.Directory.Delete(Server.MapPath("~/Images/PDF/" + id), true);
             return Json("success");
         }
         public void SavePDFImages(byte[] buffer,int EncounterID,string ImageName)
         {
             MemoryStream memStream = new MemoryStream();
             memStream.Write(buffer, 0, buffer.Length);
             Image img = Image.FromStream(memStream);          
             img.Save(Server.MapPath("~/Images/PDF/" + EncounterID + "/"+ImageName+".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
         }

         public ActionResult SearchEncounters(vwSearchPatients objView)
         {
             try
             {
                 int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
                 objView.PracticeID = PracticeID;
                 IEnumerable<vwSearchPatients> objSearchPatients = (from p in db.vwSearch

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
                 if (objView.InsuranceName != null && objView.InsuranceName != "")
                     objSearchPatients = objSearchPatients.Where(p => p.InsuranceName == objView.InsuranceName).ToList();
                 if (objView.Date_Of_Birth != null)
                     objSearchPatients = objSearchPatients.Where(p => p.Date_Of_Birth == objView.Date_Of_Birth).ToList();
                 if (objView.FromDate != null)
                     objSearchPatients = objSearchPatients.Where(p => p.FromDate >= objView.FromDate).ToList();
                 if (objView.ToDate != null)
                     objSearchPatients = objSearchPatients.Where(p => p.ToDate >= objView.FromDate).ToList();

                 //if (objSearchPatients.Count()>0)
                 //{
                 //    //return Json(objSearchPatients, JsonRequestBehavior.AllowGet);
                 //}
                 //else
                 //{
                 //    return Json(new { result = "No Data found" }, JsonRequestBehavior.AllowGet);
                 //}

                 //return Json(objView, JsonRequestBehavior.AllowGet);
                 return PartialView("PartialViews/PatEncSearchGrid", objSearchPatients);
             }
             catch (Exception ex)
             {
                 return Json(new { result = ex.Message }, JsonRequestBehavior.AllowGet);
             }

         }

         public ActionResult SaveMessage(int Encounter_ID, string Message_Desc)
         {
             Encounter_Messages enc = new Encounter_Messages();
             enc.Message_Desc = Message_Desc;
             enc.Created_Date = DateTime.Now;
             enc.Encounter_ID = Encounter_ID;
             int UserID =Convert.ToInt32(Session["sesuserid"]);
             enc.Posted_By = UserID;
             enc.Status_ID = 8;
             enc.Modified_Date = null;
             db.Enc_Msg.Add(enc);
             db.SaveChanges();
             return Json("Success");
         }

        
        
    }
}