using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class PracticeUsersController : Controller
    {
        //
        // GET: /PracticeUsers/
        private MedtecContext db = new MedtecContext();

        public ActionResult Index()
        {           
            return View();
        }

        [Authorize]
        public ActionResult Superadminsection()
        {
            return RedirectToAction("Practices", "PracticeInfo");
        }

        [Authorize]
        public ActionResult Practiceusersection()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            IEnumerable<PracticeUser> model = (from p in db.PracticeUsers
                                               where p.StatusID==1 && p.PracticeID==PracticeID
                                               select p).ToList();
            var list = new SelectList(new[]
                                          {                                              
                                              new {ID="1",Name="Provider"},
                                              new{ID="2",Name="Biller"},
                                              new{ID="3",Name="Admin"},
                                                new{ID="4",Name="Staff"}
                                          },
                           "ID", "Name", -1);
            ViewData["list"] = list;   
            return View(model);
        }    

        [HttpPost]
        public ActionResult GetEquipentData(PracticeUser objEquip)
        {
            var EquipmentsList = (from p in db.PracticeUsers
                                  where p.UserID == objEquip.UserID
                                  select new { p.UserName, p.Password, p.FirstName, p.LastName, p.MiddleName, p.Email, p.PhoneNumber, p.PracticeUserType,p.NPI }).ToList();


            return Json(EquipmentsList);
        }
        [HttpPost]
        public ActionResult CreateUser(PracticeUser objEquip)
        {
            PracticeUser objNewEquip = new PracticeUser();
            objNewEquip.UserName = objEquip.UserName;
            objNewEquip.Password = objEquip.Password;
            objNewEquip.FirstName = objEquip.FirstName;
            objNewEquip.LastName = objEquip.LastName;
            objNewEquip.MiddleName = objEquip.MiddleName;
            objNewEquip.PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            objNewEquip.Email = objEquip.Email;
            objNewEquip.PhoneNumber = objEquip.PhoneNumber;
            objNewEquip.NPI = objEquip.NPI;
            objNewEquip.PracticeUserType = objEquip.PracticeUserType;
            objNewEquip.StatusID = 1;
            db.PracticeUsers.Add(objNewEquip);
            db.SaveChanges();
            return Json(new { data = "Success" });
        }


        [HttpPost]
        public ActionResult EditUser(PracticeUser objUser)
        {
            PracticeUser objNewUser = (from p in db.PracticeUsers
                                       where p.UserID == objUser.UserID
                                        select p).FirstOrDefault();

            objNewUser.UserName = objUser.UserName;
            objNewUser.Password = objUser.Password;
            objNewUser.FirstName = objUser.FirstName;
            objNewUser.LastName = objUser.LastName;
            objNewUser.MiddleName = objUser.MiddleName;
            objNewUser.Email = objUser.Email;
            objNewUser.PhoneNumber = objUser.PhoneNumber;
            objNewUser.NPI = objUser.NPI;
            objNewUser.PracticeUserType = objUser.PracticeUserType;
            db.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public ActionResult DeleteUser(PracticeUser objEquip)
        {
            var USerlst = (from p in db.PracticeUsers
                                     where p.UserID == objEquip.UserID          
                      select new { p.UserName, p.Password, p.FirstName, p.LastName, p.MiddleName, p.Email, p.PhoneNumber, p.PracticeUserType, p.NPI ,p.StatusID}).ToList()[0];
            //PracticeUser usr = USerlst as PracticeUser;
            PracticeUser usr = db.PracticeUsers.Where(p => p.UserID == objEquip.UserID).First();
            usr.StatusID = 2;
            db.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public int CheckUserName(string UserName)
        {
            var k = from v in db.PracticeUsers
                    where v.UserName == UserName && v.StatusID!=2
                    select v;
            var res = 0;
            if(k.Count()>0)
                 res=1;//UserName already exists

            return res;
        }


    }
}
