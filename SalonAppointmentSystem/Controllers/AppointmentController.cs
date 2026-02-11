using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonAppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllAppointments()
        {
            DapperORM.ExecuteWithoutReturn("SetNoShow");
            return View();
        }
        public JsonResult AppointmentsAll()
        {
            var list = DapperORM.ReturnList<AppointmentVM>("GetAllAppointments");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Appointments()
        {
            return View();
        }

        public JsonResult MyAppointments()
        {
            if (Session["UserId"] == null)
            {
                Session.Clear();
                Session.Abandon();
                return Json(new
                {
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
            int CustomerId = Convert.ToInt32(Session["UserId"]);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CustomerId", CustomerId);
            var list = DapperORM.ReturnList<AppointmentVM>("GetAppointementsByCustomerId", dp);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatus(AppointmentVM appointment)
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@AppointmentId", appointment.AppointmentId);
                dp.Add("@Status", appointment.Status);

                DapperORM.ExecuteWithoutReturn("UpdateAppointmentStatus", dp);

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public JsonResult CancelAppointments(AppointmentVM appointment)
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@AppointmentId", appointment.AppointmentId);
                DapperORM.ExecuteWithoutReturn("CancelAppointment", dp);
                return Json(new { success = true });
            }
            catch(SqlException ex)
            {
                TempData["Error"] = ex.Message;
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}