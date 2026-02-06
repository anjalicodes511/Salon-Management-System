using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models;
using SalonAppointmentSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SalonAppointmentSystem.Controllers
{
    public class BookingController : Controller
    {
        public JsonResult GetAvailableSlots(int serviceId,DateTime date)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    Session.Abandon();
                    return Json(new
                    {
                        success = false,
                        message = "Something went wrong while fetching available slots."
                    }, JsonRequestBehavior.AllowGet);
                }
                //Delete past time slots
                DapperORM.ExecuteWithoutReturn("DeleteExpiredStylistTimeSlots");
                //Internally generating slots for each stylist
                var StylistList = DapperORM.ReturnList<StylistModel>("GetAllStylists");
                foreach (var stylist in StylistList)
                {
                    GenerateTimeSlot(stylist.StylistId, date);
                }
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@SlotDate", date);
                var SlotsStartTime = DapperORM.ReturnList<SlotTimeVM>("GetAllSlotsStartTime", dp);

                var result = SlotsStartTime.Select(s => new {
                    StartTime = s.StartTime.ToString(@"hh\:mm")
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return Json(new
                {
                    success = false,
                    message = "Please select a valid date (you can only book up to 10 days in advance)."
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return Json(new
                {
                    success = false,
                    message = "Something went wrong while fetching available slots."
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAvailableStylists(int serviceId,DateTime date,TimeSpan startTime)
        {
            if (Session["UserId"] == null)
            {
                Session.Abandon();
                return Json(new
                {
                    success = false,
                    message = "Something went wrong while fetching available slots."
                }, JsonRequestBehavior.AllowGet);
            }
            //System.Diagnostics.Debug.WriteLine("DATE: " + date.ToString("yyyy-MM-dd"));
            //System.Diagnostics.Debug.WriteLine("TIME: " + startTime);

            DynamicParameters dp = new DynamicParameters();
            dp.Add("@ServiceId", serviceId);
            dp.Add("@SlotDate", date);
            dp.Add("@StartTime", startTime);
            var result = DapperORM.ReturnList<StylistModel>("GetAvailableStylistsForDate", dp);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void GenerateTimeSlot(int StylistId, DateTime SlotDate)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@StylistId", StylistId);
            dp.Add("@SlotDate", SlotDate);
            DapperORM.ExecuteWithoutReturn("GenerateStylistSlotsForDate", dp);
        }
    
       
    }
}