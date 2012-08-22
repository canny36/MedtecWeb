using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedtecMedical_App.Models;

namespace MedtecMedical_App.Controllers
{
    public class EquipmentsInfoController : Controller
    {
        //
        // GET: /EquipmentsInfo/

        MedtecContext objDbContext;

        public EquipmentsInfoController()
        {
            objDbContext = new MedtecContext();
        }

        //[Authorize]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult Equipments()
        {
            int pracid = Convert.ToInt32(Session["sespracticeid"]);

            var EquipmentsList = (from p in objDbContext.Equipments
                                  where p.PracticeID == pracid
                                  where p.StatusID == 1
                                  select p).ToList();
            for (int i = 0; i < EquipmentsList.Count(); i++)
            {
                var EquipID = EquipmentsList.ToList()[i].EquipID;
                EquipmentsList.ToList()[i].AccesoryCount = (from k in objDbContext.EquipmentAccessorys
                                                            where k.EquipID == EquipID && k.StatusID==1
                                                            select k.AccessoryID).Count();
            }
            return View(EquipmentsList);
        }

        [HttpPost]
        public ActionResult CreateEquipent(Equipment objEquip)
        {
            Equipment objNewEquip = new Equipment();
            objNewEquip.Equip_Name = objEquip.Equip_Name;
            objNewEquip.Manufacturer = objEquip.Manufacturer;
            objNewEquip.Model = objEquip.Model;
            objNewEquip.Quantity = objEquip.Quantity;
            objNewEquip.QuantityonHand = objEquip.QuantityonHand;
            objNewEquip.PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            objNewEquip.StatusID = 1;
            objDbContext.Equipments.Add(objNewEquip);
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }


        [HttpPost]
        public ActionResult EditEquipent(Equipment objEquip)
        {
            Equipment EquipmentsList = (from p in objDbContext.Equipments
                                        where p.EquipID == objEquip.EquipID
                                        select p).FirstOrDefault();

            EquipmentsList.Equip_Name = objEquip.Equip_Name;
            EquipmentsList.Manufacturer = objEquip.Manufacturer;
            EquipmentsList.Model = objEquip.Model;
            EquipmentsList.Quantity = objEquip.Quantity;
            EquipmentsList.QuantityonHand = objEquip.QuantityonHand;

            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public ActionResult GetEquipentData(Equipment objEquip)
        {
            var EquipmentsList = (from p in objDbContext.Equipments
                                  where p.EquipID == objEquip.EquipID
                                  select p).ToList();
            return Json(EquipmentsList);
        }

        [HttpPost]
        public ActionResult DeleteEquipent(Equipment objEquip)
        {
            Equipment EquipmentsList = (from p in objDbContext.Equipments
                                        where p.EquipID == objEquip.EquipID
                                        select p).FirstOrDefault();
            EquipmentsList.StatusID = 2;
           
           IEnumerable<EquipmentAccessories> equipAcc = (from v in objDbContext.EquipmentAccessorys
                                            where v.EquipID == objEquip.EquipID
                                            select v).ToList();
           foreach (var acc in equipAcc)
           {
               acc.StatusID = 2;
           }
           objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        [Authorize]
        public ActionResult Accessories()
        {
            int PracticeID = Convert.ToInt32(Session["sespracticeid"]);
            IEnumerable<EquipmentAccessories> model;
            var EquipID = 0;
            if (Session["EQUIPID"] != null)
            {
                EquipID = Convert.ToInt32(Session["EQUIPID"]);
                model = (from p in objDbContext.EquipmentAccessorys
                         where p.StatusID == 1 && p.equip.PracticeID == PracticeID && p.EquipID == EquipID
                         select p).ToList();
                if (EquipID == 4)
                    model = (from p in objDbContext.EquipmentAccessorys
                             where p.StatusID == 1 && p.equip.PracticeID == PracticeID
                             select p).ToList();
            }
            else
            {
                model = (from p in objDbContext.EquipmentAccessorys
                         where p.StatusID == 1 && p.equip.PracticeID == PracticeID
                         select p).ToList();
            }
         
            var objEquip1 = (from v in objDbContext.Equipments
                            where v.StatusID == 1 && v.PracticeID == PracticeID
                            select v).ToList();
            ViewBag.EquipID = new SelectList(objEquip1, "EquipID", "Equip_Name", objDbContext.Equipments.First());
            Equipment eq = new Equipment();
            eq.EquipID = 4;
            eq.Equip_Name = "Select All";
            var objEquip = (from v in objDbContext.Equipments
                            where v.StatusID == 1 && v.PracticeID == PracticeID
                            select v).ToList();
            //ViewBag.EquipID = new SelectList(objEquip, "EquipID", "Equip_Name", objDbContext.Equipments.First());
            objEquip.Insert(0, eq);
            if (Session["EQUIPID"] == null)
            ViewBag.EquipID1 = new SelectList(objEquip, "EquipID", "Equip_Name", objDbContext.Equipments.First());
            else
             ViewBag.EquipID1 = new SelectList(objEquip, "EquipID", "Equip_Name", EquipID);

            return View(model);

        }

        [HttpPost]
        public ActionResult GetEquipentAccessoryData(EquipmentAccessories objEquip)
        {
            var EquipmentsList = (from p in objDbContext.EquipmentAccessorys
                                  where p.AccessoryID == objEquip.AccessoryID
                                  select new {p.AccessoryName, p.equip.Equip_Name, p.PartNumber, p.SupplyQuantity, p.QuantityonHand, p.Manufacturer }).ToList();


            return Json(EquipmentsList);
        }
        [HttpPost]
        public ActionResult CreateAccessory(EquipmentAccessories objEquip)
        {
            EquipmentAccessories objNewEquip = new EquipmentAccessories();
            objNewEquip.EquipID = objEquip.EquipID;
            objNewEquip.AccessoryName = objEquip.AccessoryName;
            objNewEquip.Manufacturer = objEquip.Manufacturer;
            objNewEquip.PartNumber = objEquip.PartNumber;
            objNewEquip.QuantityonHand = objEquip.QuantityonHand;
            objNewEquip.SupplyQuantity = objEquip.SupplyQuantity;
            objNewEquip.StatusID = 1;
            objDbContext.EquipmentAccessorys.Add(objNewEquip);
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public ActionResult EditAccessory(EquipmentAccessories objEquip)
        {
            EquipmentAccessories objNewEquip = (from p in objDbContext.EquipmentAccessorys
                                                where p.AccessoryID == objEquip.AccessoryID
                                                select p).FirstOrDefault();

            objNewEquip.EquipID = objEquip.EquipID;
            objNewEquip.AccessoryName = objEquip.AccessoryName;
            objNewEquip.Manufacturer = objEquip.Manufacturer;
            objNewEquip.PartNumber = objEquip.PartNumber;
            objNewEquip.QuantityonHand = objEquip.QuantityonHand;
            objNewEquip.SupplyQuantity = objEquip.SupplyQuantity;
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        [HttpPost]
        public ActionResult DeleteAccessory(EquipmentAccessories objEquip)
        {
            var USerlst = (from p in objDbContext.EquipmentAccessorys
                           where p.AccessoryID == objEquip.AccessoryID
                           select new { p.EquipID,p.AccessoryName, p.PartNumber, p.SupplyQuantity, p.QuantityonHand, p.Manufacturer, p.StatusID }).ToList()[0];
            EquipmentAccessories usr = objDbContext.EquipmentAccessorys.Where(p => p.AccessoryID == objEquip.AccessoryID).First();
            usr.StatusID = 2;
            objDbContext.SaveChanges();
            return Json(new { data = "Success" });
        }

        public ActionResult ddlEquipSelectionChanged(int EquipID)
        {
            Session["EQUIPID"] = EquipID;
            return Json(new { data = "Success" });
        }

      
        public ActionResult GetAccessories(int EquipID)
        {
            var s=from k in objDbContext.Equipments
                  where k.EquipID==EquipID
                  select k.Equip_Name;
            var Title = s.First();
            var AccessoriesList = (from p in objDbContext.EquipmentAccessorys
                                   where p.EquipID == EquipID
                                  where p.StatusID == 1
                                select new {p.SupplyQuantity,
                                            p.QuantityonHand,
                                            p.PartNumber,
                                            AccessoryTitle = Title
                                }).ToList();
          
            return Json(AccessoriesList,JsonRequestBehavior.AllowGet);
        }
    }
}
