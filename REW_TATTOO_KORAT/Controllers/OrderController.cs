using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using REW_TATTOO_KORAT.Models;


namespace REW_TATTOO_KORAT.Controllers
{
    public class OrderController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: Order
        public ActionResult Index()
        {
            var tattoos = db.Tattoos.Include(t => t.Tattoo_Type);
            return View(tattoos.ToList());
        }

        // GET: AdminOder/Create
        public ActionResult Create(int? id)
        {
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName");
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tattoo tattoo = db.Tattoos.Find(id);
            if (tattoo == null)
            {
                return HttpNotFound();
            }
            return View(tattoo);
        }

        // POST: AdminOder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "O_ID,O_Date,UserName,Do_Date,Tattoo_ID,Price,Description,Status")] OderMaster oderMaster)
        {
            if (Session["UserName"] != null)
            {
                if (oderMaster.O_Date == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        oderMaster.Do_Date = DateTime.Now;
                        oderMaster.UserName = Session["UserName"].ToString();
                        oderMaster.Status = "ค้างชำระ";
                        db.OderMasters.Add(oderMaster);
                        db.SaveChanges();
                        return RedirectToAction("Index", "OderMasters");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", oderMaster.UserName);
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name", oderMaster.Tattoo_ID);
            return View(oderMaster);
        }

    }
}