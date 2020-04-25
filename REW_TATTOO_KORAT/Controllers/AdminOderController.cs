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
    public class AdminOderController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: AdminOder
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            Session["User_Id"] = true;
            var oderMasters = db.OderMasters.Include(o => o.Member).Include(o => o.Tattoo);
            return View(oderMasters.ToList());
        }

        // GET: AdminOder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OderMaster oderMaster = db.OderMasters.Find(id);
            if (oderMaster == null)
            {
                return HttpNotFound();
            }
            return View(oderMaster);
        }

        // GET: AdminOder/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName");
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name");
            return View();
        }

        // POST: AdminOder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "O_ID,O_Date,UserName,Do_Date,Tattoo_ID,Price,Description,Status")] OderMaster oderMaster)
        {
            if (ModelState.IsValid)
            {
                db.OderMasters.Add(oderMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", oderMaster.UserName);
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name", oderMaster.Tattoo_ID);
            return View(oderMaster);
        }

        // GET: AdminOder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OderMaster oderMaster = db.OderMasters.Find(id);
            if (oderMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", oderMaster.UserName);
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name", oderMaster.Tattoo_ID);
            return View(oderMaster);
        }

        // POST: AdminOder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "O_ID,O_Date,UserName,Do_Date,Tattoo_ID,Price,Description,Status")] OderMaster oderMaster)
        {
            var update = db.OderMasters.Where(x => x.O_ID == oderMaster.O_ID).ToList();
            if (update.Count() > 0)
            {
                update.ForEach(x => { x.Status = oderMaster.Status; });
                db.SaveChanges();
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", oderMaster.UserName);
            ViewBag.Tattoo_ID = new SelectList(db.Tattoos, "Tattoo_ID", "Tattoo_Name", oderMaster.Tattoo_ID);
            return RedirectToAction("Index");
        }

        // GET: AdminOder/Delete/5
        public ActionResult Delete(int? id)
        {
            OderMaster oderMaster = db.OderMasters.Find(id);
            db.OderMasters.Remove(oderMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminOder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OderMaster oderMaster = db.OderMasters.Find(id);
            db.OderMasters.Remove(oderMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
