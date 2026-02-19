using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            return View();
        }

        public ActionResult MyDetails()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Customer")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetMyDetails()
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
            int customerId = Convert.ToInt32(Session["UserId"]);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CustomerId", customerId);
            var customer = DapperORM.ReturnSingle<CustomerDetailsVM>("GetCustomerById",dp);
            return Json(customer,JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMyDetails()
        {
            return View();
        }

        public JsonResult EditDetails()
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
            int customerId = Convert.ToInt32(Session["UserId"]);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CustomerId", customerId);
            var customer = DapperORM.ReturnSingle<CustomerDetailsVM>("GetCustomerById",dp);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDetails(CustomerDetailsVM customer)
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
            try
            {
                customer.CustomerId = Convert.ToInt32(Session["UserId"]);

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@CustomerId", customer.CustomerId);
                dp.Add("@FullName", customer.FullName);
                dp.Add("@Email", customer.Email);
                dp.Add("@Phone", customer.Phone);
                dp.Add("@Gender", customer.Gender);

                DapperORM.ExecuteWithoutReturn("UpdateCustomerDetails", dp);
                return Json(new { success = true });
            }
            catch (SqlException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
    }
}