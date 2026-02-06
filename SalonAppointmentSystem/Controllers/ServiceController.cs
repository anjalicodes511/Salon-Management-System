using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonAppointmentSystem.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            
            var list = DapperORM.ReturnList<ServicesVM>("GetAllServices");
            return View(list);
        }

        public ActionResult AddOrEditServices(int id = 0)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            if (id == 0)
            {
                return View(new ServiceModel());
            }
            else
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@ServiceId", id);
                var result = DapperORM.ReturnSingle<ServiceModel>("GetServiceById", dp);
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditServices(ServiceModel service)
        {
            
            if (ModelState.IsValid)
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@ServiceId", service.ServiceId);
                dp.Add("@ServiceName",service.ServiceName);
                dp.Add("@ServiceDuration", service.ServiceDuration);
                dp.Add("@Price", service.Price);
                dp.Add("@IsActive", service.IsActive);

                DapperORM.ExecuteWithoutReturn("AddOrEditService", dp);
                return RedirectToAction("Index","Service");
            }
            else
            {
                TempData["Error"] = "Enter Valid Details!";
                return View(service);
            }
        }
    }
}