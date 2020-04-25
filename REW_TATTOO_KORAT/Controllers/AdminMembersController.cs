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
    public class AdminMembersController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: AdminMembers
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            Session["User_Id"] = true;
            return View(db.Members.ToList());
        }

        // GET: AdminMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: AdminMembers/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ErrorCode = TempData["shortMessage"].ToString();
            }
            catch
            {
                ViewBag.ErrorCode = "";
            }
            return View();
        }

        // POST: AdminMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,FirstName,LastName,Address,Tel,EMail,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Index/");
                }
                catch
                {
                    TempData["shortMessage"] = "UserName นี้ถูกใช้งานแล้ว";
                    return RedirectToAction("Create/");
                }
            }

            return View(member);
        }

        // GET: AdminMembers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: AdminMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,FirstName,LastName,Address,Tel,EMail,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: AdminMembers/Delete/5
        public ActionResult Delete(string id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
