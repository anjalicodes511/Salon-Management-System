using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models.Enums;
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
            var list = DapperORM.ReturnList<CustomerDetailsVM>("GetAllCustomers");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    
        public ActionResult CustomerProfile(int id)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public JsonResult GetCustomerProfile(int customerId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CustomerId", customerId);
            var customer = DapperORM.ReturnSingle<CustomerDetailsVM>("GetCustomerById", param);
            //var customer = customers.Where(x => x.CustomerId == customerId).FirstOrDefault();

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CustomerId", customerId);
            var appointments = DapperORM.ReturnList<AppointmentVM>("GetAppointementsByCustomerId", dp);

            var statistics = new
            {
                total = appointments.Count(),
                pending = appointments.Count(x => x.Status == AppointmentStatus.Booked),
                cancelled = appointments.Count(x => x.Status == AppointmentStatus.Cancelled),
                completed = appointments.Count(x => x.Status == AppointmentStatus.Completed),
                absent = appointments.Count(x => x.Status == AppointmentStatus.NoShow)
            };

            return Json(new { customer, appointments, statistics }, JsonRequestBehavior.AllowGet);
        }


    }
}