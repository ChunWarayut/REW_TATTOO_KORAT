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
    public class AdminTypeController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: AdminType
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            Session["User_Id"] = true;
            return View(db.Tattoo_Type.ToList());
        }

        // GET: AdminType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tattoo_Type tattoo_Type = db.Tattoo_Type.Find(id);
            if (tattoo_Type == null)
            {
                return HttpNotFound();
            }
            return View(tattoo_Type);
        }

        // GET: AdminType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type_ID,Type_Name")] Tattoo_Type tattoo_Type)
        {
            if (ModelState.IsValid)
            {
                db.Tattoo_Type.Add(tattoo_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tattoo_Type);
        }

        // GET: AdminType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tattoo_Type tattoo_Type = db.Tattoo_Type.Find(id);
            if (tattoo_Type == null)
            {
                return HttpNotFound();
            }
            return View(tattoo_Type);
        }

        // POST: AdminType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Type_ID,Type_Name")] Tattoo_Type tattoo_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tattoo_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tattoo_Type);
        }

        // GET: AdminType/Delete/5
        public ActionResult Delete(int? id)
        {
            Tattoo_Type tattoo_Type = db.Tattoo_Type.Find(id);
            db.Tattoo_Type.Remove(tattoo_Type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tattoo_Type tattoo_Type = db.Tattoo_Type.Find(id);
            db.Tattoo_Type.Remove(tattoo_Type);
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
