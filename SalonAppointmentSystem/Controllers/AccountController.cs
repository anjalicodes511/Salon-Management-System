using Dapper;
using SalonAppointmentSystem.DAL;
using SalonAppointmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SalonAppointmentSystem.Models.ViewModels;

namespace SalonAppointmentSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(CustomerModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Enter Valid Details";
                    return View(model);
                }

                PasswordHasher hasher = new PasswordHasher();
                string HashedPassword = hasher.HashPassword(model.Password);

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Role", model.Role);
                dp.Add("@FullName", model.FullName);
                dp.Add("@Email", model.Email);
                dp.Add("@Phone", model.Phone);
                dp.Add("@Gender", model.Gender);
                dp.Add("@IsActive", model.IsActive);
                dp.Add("@Password", HashedPassword);

                DapperORM.ExecuteWithoutReturn("AddCustomer", dp);
                return RedirectToAction("Login", "Account");
            }
            catch(SqlException ex)
            {
                TempData["Error"] = ex.Message;
                return View(model);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Enter Valid Detail";
                    return View(user);
                }
                
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Email", user.Email);

                var result = DapperORM.ReturnSingle<CustomerModel>("GetCustomer", dp);
                if(result == null)
                {
                    TempData["Error"] = "Invalid Email or Password";
                    return View(user);
                }
                PasswordHasher hasher = new PasswordHasher();
                var Verify = hasher.VerifyHashedPassword(result.Password, user.Password);
                if(Verify == PasswordVerificationResult.Success)
                {
                    Session["Username"] = result.FullName;
                    Session["Role"] = result.Role;
                    Session["UserId"] = result.CustomerId;

                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    TempData["Error"] = "Invalid Email or Password";
                    return View(user);
                }
            }
            catch (SqlException ex)
            {
                TempData["Error"] = ex.Message;
                return View(user);
            }
        }

        //[HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}