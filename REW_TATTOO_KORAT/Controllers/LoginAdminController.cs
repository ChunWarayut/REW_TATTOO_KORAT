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
    public class LoginAdminController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(REW_TATTOO_KORAT.Models.Member memberModel)
        {
            var memberDetail = db.Members.Where(x => x.UserName == memberModel.UserName && x.Password == memberModel.Password).FirstOrDefault();
            if (memberDetail == null)
            {
                ViewBag.LoginErrorMessage = "Email หรือ เบอร์โทรศัพท์ไม่ถูกต้อง";
                return View("Index", memberModel);
            }
            else
            {
                Session["UserName"] = memberDetail.UserName;
                Session["EMail"] = memberDetail.EMail;
                Session["Address"] = memberDetail.Address;
                Session["FirstName"] = memberDetail.FirstName;
                Session["LastName"] = memberDetail.LastName;
                Session["Tel"] = memberDetail.Tel;
                return RedirectToAction("Index", "AdminTattoos");
            }
        }

        // GET: members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,FirstName,LastName,Address,Tel,EMail,Password")] Member member, REW_TATTOO_KORAT.Models.Member memberModel)
        {
            try
            {

                var emailChecked = new System.Net.Mail.MailAddress(member.EMail);
                if (member.Tel.Length < 10)
                {
                    var phoneChecked = new System.Net.Mail.MailAddress(member.Tel);
                }
                if (ModelState.IsValid)
                {
                    Session["UserName"] = memberModel.UserName;
                    Session["EMail"] = memberModel.EMail;
                    Session["Address"] = memberModel.Address;
                    Session["FirstName"] = memberModel.FirstName;
                    Session["LastName"] = memberModel.LastName;
                    Session["Tel"] = memberModel.Tel;
                    db.Members.Add(memberModel);
                    db.SaveChanges();
                    var memberDetail = db.Members.OrderByDescending(x => x.UserName).FirstOrDefault();
                    Session["UserName"] = memberDetail.UserName;
                    Session["EMail"] = memberDetail.EMail;
                    Session["Address"] = memberDetail.Address;
                    Session["FirstName"] = memberDetail.FirstName;
                    Session["LastName"] = memberDetail.LastName;
                    Session["Tel"] = memberDetail.Tel;
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.ErrorCHK = "True";
                ViewBag.LoginErrorMessageTEL = "เบอร์โทรต้องมี 10 หลัก";
                ViewBag.LoginErrorMessageEMAIL = "กรุณาตรวจสอบ Email";
                ViewBag.LoginErrorMessageUSERNAME = "UseerName นี้ถูกใช้แล้ว";
                return View(memberModel);
            }
        }



        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}