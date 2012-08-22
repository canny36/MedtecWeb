using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class PracticeInfoController : Controller
    {



        MedtecContext objDbContext;

        public PracticeInfoController()
        {
            objDbContext = new MedtecContext();
        }
        //
        // GET: /PracticeInfo/

        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Practices(string sort)
        {

            var practices = (from p in objDbContext.vwPractices
                             where p.StatusID == 1
                             select p).ToList();

            switch (sort)
            {
                case "PracticeName":
                    practices = practices.OrderBy(r => r.PracticeName).ToList();
                    break;
                case "Description":
                    practices = practices.OrderBy(r => r.Description).ToList();
                    break;
                case "Email":
                    practices = practices.OrderBy(r => r.Email).ToList();
                    break;
                case "PhoneNumber":
                    practices = practices.OrderBy(r => r.PhoneNumber).ToList();
                    break;
                default:
                    practices = practices.OrderBy(r => r.PracticeID).ToList();
                    break;
            }


            return View(practices);


        }

        [HttpPost]
        public ActionResult CreatePractice(vwPractice objprac)
        {
            Practice pra = new Practice();
            pra.PracticeID = objprac.PracticeID;
            pra.PracticeName = objprac.PracticeName;
            pra.NPI = objprac.NPI;
            pra.PhoneNumber = objprac.PhoneNumber;
            pra.State = objprac.State;
            pra.TAXID = objprac.TAXID;
            pra.ZipeCode = objprac.ZipeCode;
            pra.Address1 = objprac.Address1;
            pra.Address2 = objprac.Address2;
            pra.City = objprac.City;
            pra.Description = objprac.Description;
            pra.StatusID = objprac.StatusID;
            pra.Email = objprac.Email;
            pra.StatusID = 1;
            objDbContext.Practices.Add(pra);
            objDbContext.SaveChanges();

            int lastPracticeId = objDbContext.Practices.Max(item => item.PracticeID);

            PracticeUser temp = new PracticeUser();
            //temp = objDbContext.PracticeUsers.Find(objprac.UserID);
            temp.UserName = objprac.UserName;
            temp.Password = objprac.Password;
            temp.PracticeUserType = "Admin";
            temp.Email = objprac.Email;
            temp.PhoneNumber = objprac.PhoneNumber;
            temp.PracticeID = lastPracticeId;
            temp.StatusID = 1;
            objDbContext.PracticeUsers.Add(temp);
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }


        [HttpPost]
        public ActionResult EditPractice(vwPractice objprac)
        {
            Practice pra = (from p in objDbContext.Practices
                            where p.PracticeID == objprac.PracticeID
                            select p).FirstOrDefault();
            pra.PracticeID = objprac.PracticeID;
            pra.PracticeName = objprac.PracticeName;
            pra.NPI = objprac.NPI;
            pra.PhoneNumber = objprac.PhoneNumber;
            pra.State = objprac.State;
            pra.TAXID = objprac.TAXID;
            pra.ZipeCode = objprac.ZipeCode;
            pra.Address1 = objprac.Address1;
            pra.Address2 = objprac.Address2;
            pra.City = objprac.City;
            pra.Description = objprac.Description;
            pra.StatusID = 1;
            pra.Email = objprac.Email;
            PracticeUser temp = new PracticeUser();
            temp = objDbContext.PracticeUsers.Find(objprac.UserID);
            temp.UserName = objprac.UserName;
            temp.Password = objprac.Password;
            temp.PracticeUserType = "Admin";

            //if (ModelState.IsValid)
            //{
            //    objDbContext.Entry(pra).State = EntityState.Modified;
            //    objDbContext.Entry(temp).State = EntityState.Modified;
            //    objDbContext.SaveChanges();
            //    return RedirectToAction("Superadminsection");
            //}

            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }


        [HttpPost]
        public ActionResult GetPracticesData(vwPractice objpractice)
        {
            var practiceslist = (from p in objDbContext.vwPractices
                                 where p.PracticeID == objpractice.PracticeID
                                 select p).ToList();


            return Json(practiceslist);
        }


        [HttpPost]
        public ActionResult DeletePractice(vwPractice objprac)
        {
            var practiceUsers = (from p in objDbContext.PracticeUsers
                                 where p.PracticeID == objprac.PracticeID
                                 select p).ToList();
            foreach (var n in practiceUsers)
            {
                n.StatusID = 2;
            }
            Practice pra = (from p in objDbContext.Practices
                            where p.PracticeID == objprac.PracticeID
                            select p).FirstOrDefault();
            pra.StatusID = 2;
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]

        public string CheckAvailabilty(string UserName)
        {
            string data = "";
            var practiceUsers = (from p in objDbContext.vwPracticeUsers
                                 where p.UserName == UserName.Trim()
                                 select p).ToList();
            if (practiceUsers.Count == 0)
            {
                data = "available";
            }
            else
            {
                data = "unavailable";
            }

            return data;
        }


        public void Launch(int PracticeID)
        {
            Session["sesuserid"] = 0;
            Session["sesusername"] = "superadmin";
            Session["sesuserrole"] = "superadmin";
            Session["sespracticename"] = from v in objDbContext.Practices
                                         where v.PracticeID == PracticeID
                                         select v.PracticeName;
            Session["sespracticeid"] = PracticeID;
        }
    }
}
