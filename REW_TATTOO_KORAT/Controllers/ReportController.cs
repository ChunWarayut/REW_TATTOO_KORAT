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
    public class ReportController : Controller
    {
        private REW_TATTOO_DBEntities db = new REW_TATTOO_DBEntities();
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        // GET: Report
        public ActionResult Type()
        {
            return View(db.Tattoo_Type.ToList());
        }
        // GET: Report
        public ActionResult Tattoo()
        {
            return View(db.Tattoos.ToList());
        }
        // GET: Report
        public ActionResult Booking()
        {
            return View(db.OderMasters.Where(x => x.Status != "ยกเลิก").ToList());
        }
        // GET: Report
        public ActionResult CancelBooking()
        {
            return View(db.OderMasters.Where(x => x.Status == "ยกเลิก").ToList());
        }
    }
}