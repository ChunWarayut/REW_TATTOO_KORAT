using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using REW_TATTOO_KORAT.Models;

namespace REW_TATTOO_KORAT.Controllers
{
    public class AdminTattoosController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: AdminTattoos
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            Session["User_Id"] = true;
            var tattoos = db.Tattoos.Include(t => t.Tattoo_Type);
            return View(tattoos.ToList());
        }

        // GET: AdminTattoos/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: AdminTattoos/Create
        public ActionResult Create()
        {
            ViewBag.Type_ID = new SelectList(db.Tattoo_Type, "Type_ID", "Type_Name");
            return View();
        }

        // POST: AdminTattoos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tattoo_ID,Tattoo_Name,Tattoo_Pic,Size,Unit_Price,Type_ID")] Tattoo tattoo, HttpPostedFileBase Tattoo_Pic)
        {
            try
            {
                if (Tattoo_Pic.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(Tattoo_Pic.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                    Tattoo_Pic.SaveAs(FolderPath);
                    tattoo.Tattoo_Pic = FileName; 
                        db.Tattoos.Add(tattoo);
                        db.SaveChanges();
                        return RedirectToAction("Index"); 
                }

            }
            catch
            {
                tattoo.Tattoo_Pic = "default.png";
                db.Tattoos.Add(tattoo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type_ID = new SelectList(db.Tattoo_Type, "Type_ID", "Type_Name", tattoo.Type_ID);
            return View(tattoo);
        }

        // GET: AdminTattoos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tattoo tattoo = db.Tattoos.Find(id);
            if (tattoo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_ID = new SelectList(db.Tattoo_Type, "Type_ID", "Type_Name", tattoo.Type_ID);
            return View(tattoo);
        }

        // POST: AdminTattoos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tattoo_ID,Tattoo_Name,Tattoo_Pic,Size,Unit_Price,Type_ID")] Tattoo tattoo, HttpPostedFileBase Tattoo_Pic, string nopic)
        {
            try
            {
                if (Tattoo_Pic.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(Tattoo_Pic.FileName);
                    string FolderPath = Path.Combine(Server.MapPath("~/image"), FileName);
                    Tattoo_Pic.SaveAs(FolderPath);
                    tattoo.Tattoo_Pic = FileName;

                    db.Entry(tattoo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                tattoo.Tattoo_Pic = nopic;
                db.Entry(tattoo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_ID = new SelectList(db.Tattoo_Type, "Type_ID", "Type_Name", tattoo.Type_ID);
            return View(tattoo);
        }

        // GET: AdminTattoos/Delete/5
        public ActionResult Delete(int? id)
        {
            Tattoo tattoo = db.Tattoos.Find(id);
            db.Tattoos.Remove(tattoo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminTattoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tattoo tattoo = db.Tattoos.Find(id);
            db.Tattoos.Remove(tattoo);
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
