﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class ViewPatientsInfoController : Controller
    {
        //
        // GET: /ViewPatientsInfo/
        private MedtecContext db = new MedtecContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewPatients()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);       
            IEnumerable<vwPatient_PatientEncounters> model = (from p in db.vwPatients
                                         where p.StatusID == 5 && p.PracticeID == PracticeID
                                         select p).Distinct();                   
            return View(model);
        }

        public ViewResult ViewPatientDetails(int id)
        {
            IEnumerable<PatientEncounters> model = (from p in db.PatientEncountersList
                                                    where p.StatusID == 5 && p.PatientID == id
                                                    select p).ToList();
            db.PatientEncountersList.Find(id);
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

        public ActionResult ViewPatientEncounters(int id)
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);

            ViewBag.StatusID = from v in db.StatusList
                               where v.StatusID == 3 || v.StatusID == 4 || v.StatusID == 5
                               select v;
            IEnumerable<Encounter_Accessories> patientenc = (from p in db.Encounter_AccessoriesList
                                                             where p.EncounterID == id && p.patientenc.equip.PracticeID == PracticeID
                                                             select p).ToList();
            Encounter_Accessories enc = new Encounter_Accessories();
            if (patientenc.Count() == 0)
            {
                List<Encounter_Accessories> patient = new List<Encounter_Accessories>();
                enc.EncAccID = enc.EncounterID = enc.AccessoryID = enc.Quantity = 0;
                enc.patientenc = db.PatientEncountersList.Find(id);
                patient.Add(enc);
                return View(patient);
            }
            else
                return View(patientenc);
        }
              [HttpPost]
        public ActionResult UpdateNotes(PatientEncounters enc)
        {
            PatientEncounters objNewEquip = (from p in db.PatientEncountersList
                                                where p.EncounterID == enc.EncounterID
                                                select p).FirstOrDefault();
            if (Session["sesuserrole"].ToString() == "Biller")
            objNewEquip.Notes = enc.Notes;
                  objNewEquip.StatusID=enc.StatusID;
            db.SaveChanges();
            return Json(new { data = "Success" });
        }
    }
}
