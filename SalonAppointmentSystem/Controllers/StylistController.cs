using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalonAppointmentSystem.Controllers
{
    public class StylistController : Controller
    {
        // GET: Stylist
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(Session["Role"] != null && Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Index", "Account");
            }
            var list = DapperORM.ReturnList<StylistVM>("GetAllStylists");
            return View(list);
        }

        public ActionResult AddOrEditStylist(int id = 0)
        {
            if (Session["UserId"] == null || Session["Role"].ToString() != "Admin")
            {
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            if (id == 0)
            {
                return View(new StylistModel());
            }
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@StylistId", id);
            var result = DapperORM.ReturnSingle<StylistModel>("GetStylistById", dp);
            return View(result);
        }

        [HttpPost]
        public ActionResult AddOrEditStylist(StylistModel stylist)
        {
            if (ModelState.IsValid)
            {
                
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@StylistId", stylist.StylistId, DbType.Int32, ParameterDirection.InputOutput);
                dp.Add("@StylistName", stylist.StylistName);
                dp.Add("@StartTime", stylist.StartTime);
                dp.Add("@EndTime", stylist.EndTime);
                dp.Add("@IsActive", stylist.IsActive);

                DapperORM.ExecuteWithoutReturn("AddOrEditStylist", dp);
                stylist.StylistId = dp.Get<int>("@StylistId");
                StylistDailySchedule(stylist);
                return RedirectToAction("Index", "Stylist");
            }
            else
            {
                TempData["Error"] = "Enter Valid Details";
                return View(stylist);
            }
        }
        public void StylistDailySchedule(StylistModel stylist)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@StylistId", stylist.StylistId);
            dp.Add("@StartTime", stylist.StartTime);
            dp.Add("@EndTime", stylist.EndTime);
            dp.Add("@IsActive", stylist.IsActive);
            dp.Add("@SlotDuration", 30);
            dp.Add("@BreakStart", new TimeSpan(13, 0, 0)); // 1:00 PM
            dp.Add("@BreakEnd", new TimeSpan(14, 0, 0)); // 2:00 PM

            DapperORM.ExecuteWithoutReturn("GenerateStylistDailySchedule", dp);

        }
    }
}