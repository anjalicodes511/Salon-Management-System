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
    public class StylistServiceController : Controller
    {
        // GET: StylistService
        public ActionResult Details(int id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@StylistId", id);
            var result = DapperORM.ReturnSingle<StylistModel>("GetStylistById", dp);
            
            if(result == null)
            {
                return HttpNotFound();
            }
            var services = DapperORM.ReturnList<StylistServiceVM>("GetAssignedServicesOfStylist", dp);
            var model = new StylistDetailsVM
            {
                StylistId = result.StylistId,
                StylistName = result.StylistName,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                Services = services.ToList()
            };
            return View(model);
        }
        public ActionResult Assign(int id)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            ViewBag.StylistId = id;
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@StylistId", id);
            var result = DapperORM.ReturnList<StylistServiceVM>("GetAllServicesForStylist", dp);
            return View(result);
        }

        [HttpPost]
        public ActionResult Assign(int StylistId, int[] SelectedServices)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Select Valid Details";
                return View();
            }
            DynamicParameters dp = new DynamicParameters();
            dp.Add("StylistId", StylistId);
            DapperORM.ExecuteWithoutReturn("RemoveAllServicesForStylist", dp);

            if(SelectedServices != null)
            {
                foreach(int ServiceId in SelectedServices)
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@StylistId", StylistId);
                    param.Add("@ServiceId", ServiceId);
                    DapperORM.ExecuteWithoutReturn("AssignServicesToStylist", param);
                }
            }
            return RedirectToAction("Details", "StylistService", new {id = StylistId});
        }

    }
}