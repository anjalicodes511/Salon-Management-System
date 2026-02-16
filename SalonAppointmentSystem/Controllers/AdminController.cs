using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SalonAppointmentSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult CustomerDetails()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public JsonResult GetCustomerDetails()
        {
            try
            {
                var list = DapperORM.ReturnList<CustomerDetailsVM>("GetAllCustomers");
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}