using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonAppointmentSystem.Controllers
{
    public class ReportController : Controller
    {
        //GET: Report
        public ActionResult Index()
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public JsonResult GetAppointmentsReport()
        {
            try
            {
                var appointments = DapperORM.ReturnList<AppointmentReportVM>("GetAppointmentReport");

                var result = new
                {
                    total = appointments.Count(),
                    booked = appointments.Count(x => x.Status == 1),
                    cancelled = appointments.Count(x => x.Status == 2),
                    completed = appointments.Count(x => x.Status == 3),
                    absent = appointments.Count(x => x.Status == 4),

                    recentAppointments = appointments.OrderByDescending(x => x.AppointmentDate).Take(5).Select(x => new
                    {
                        date = x.AppointmentDate.ToString("dd-MM-yyyy"),
                        customer = x.CustomerName,
                        service = x.ServiceName,
                        status = GetStatusText(x.Status),
                        stylist = x.StylistName
                    })
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Something Went Wrong"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetStatusText(int status)
        {
            switch (status)
            {
                case 1: return "Booked";
                case 2: return "Cancelled";
                case 3: return "Completed";
                case 4: return "Absent";
                default: return "Unknown";
            }
        }

        public JsonResult GetRevenueReport()
        {
            try
            {
                var list = DapperORM.ReturnList<RevenueReportVM>("GetRevenueReport");

                var result = new
                {
                    totalRevenue = list.Sum(x => x.TotalRevenue),
                    serviceRevenue = list.Select(x => new
                    {
                        service = x.ServiceName,
                        bookings = x.TotalBookings,
                        revenue = x.TotalRevenue
                    })
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Something Went Wrong"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMonthlyRevenueReport()
        {
            try
            {
                var list = DapperORM
                    .ReturnList<MonthlyRevenueVM>("GetMonthlyRevenueReport")
                    .ToList();

                return Json(new
                {
                    months = list.Select(x => x.Month),
                    revenue = list.Select(x => x.TotalRevenue)
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Something Went Wrong"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPopularServicesReport()
        {
            try
            {
                var list = DapperORM.ReturnList<TopServiceVM>("GetPopularServicesReport");
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Something Went Wrong"
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}