using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonAppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
    }
}