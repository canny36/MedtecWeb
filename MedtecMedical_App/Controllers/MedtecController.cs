using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MedtecMedical_App.Models;
using System.Data.Entity.Infrastructure;

namespace MedtecMedical_App.Controllers
{
    public class MedtecController : Controller
    {
        //
        // GET: /Medtec/

        MedtecContext objDBContect;
        public MedtecController()
        {
            objDBContect = new MedtecContext();
        }

        [Authorize]
        public ActionResult Index()
        {          
            return View();
        }

        public void PopulatePracticeDropdown()
        {
            var PracticesList = new List<string>();

            var dbPracticeListItems = (from p in objDBContect.Practices    
                                       where p.StatusID!=2
                                       where p.StatusID == 1
                                       select p.PracticeName).ToList();

            if (dbPracticeListItems != null)
            {
                PracticesList.AddRange(dbPracticeListItems.Distinct());
                ViewBag.ddlPractice = new SelectList(PracticesList);
            }
        }
      
        public ActionResult Login()
        {
            PopulatePracticeDropdown();

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection objFrm,string returnUrl)
        {
            string username = objFrm["txtUsername"];
            string password = objFrm["txtPassword"];
            string practice = objFrm["ddlPractice"];

            string strSuperAdminDetails = GetUsernamerPassword();

            if (username != "" && password != "")
            {
                if (practice != "")
                {

                    var varPractice = (from p in objDBContect.vwPracticeUsers
                                       where p.UserName == username
                                       where p.Password == password
                                       where p.PracticeName == practice                                       
                                       select p).FirstOrDefault();

                    if (varPractice != null)
                    {
                        Session["sesuserid"] = varPractice.UserID;
                        Session["sesusername"] = varPractice.FirstName;
                        Session["sesuserrole"] = varPractice.PracticeUserType;
                        Session["sespracticename"] = varPractice.PracticeName;
                        Session["sespracticeid"] = varPractice.PracticeID;

                        FormsAuthentication.SetAuthCookie(username, false);

                        if (varPractice.PracticeUserType == "Admin")
                        {
                            return RedirectToAction("Practiceusersection", "PracticeUsers");
                        }
                        else if (varPractice.PracticeUserType == "Provider")
                        {
                            return RedirectToAction("Practiceusersection", "PracticeUsers");
                        }
                        else if (varPractice.PracticeUserType == "Staff")
                        {
                            return RedirectToAction("patients", "patientinfo");
                        }
                        else if (varPractice.PracticeUserType == "Biller")
                        { 
                            return RedirectToAction("viewpatients", "Viewpatientsinfo");
                        }
                        else 
                        {
                            return RedirectToAction("Practiceusersection", "PracticeUsers");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username/Password");
                    }                   
                   
                }
                else
                {
                    string LoginInfo=username + "," + password;
                    if (strSuperAdminDetails == LoginInfo)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        Session["sesusername"] = "superadmin";
                        Session["sesuserrole"] = "SuperAdmin";
                        return RedirectToAction("Superadminsection", "PracticeUsers");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Credentials");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter valid username/password");
            }

            PopulatePracticeDropdown();
            return View();
        }

        public string GetUsernamerPassword()
        {
            int counter = 0;
            string line;
            string usernamepassword = "";
            System.IO.StreamReader file =
               new System.IO.StreamReader(Server.MapPath("~/SuperAdminLogin.txt"));
            while ((line = file.ReadLine()) != null)
            {
                usernamepassword = line;
                counter++;
            }
            file.Close();
            return usernamepassword;
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Medtec");
        }

    }
}
