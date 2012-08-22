using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class AlertsController : Controller
    {
        //
        // GET: /Alerts/
        MedtecContext db;
        public AlertsController()
        {
            db = new MedtecContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Alerts()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            IEnumerable<vwPatient_PatientEncounters> objPatients = (from v in db.vwPatients
                                                                    where v.PracticeID == PracticeID && (v.StatusID == 3 || v.StatusID == 4)
                                                                    select v).ToList();
            return View(objPatients);

        }
    }
}
