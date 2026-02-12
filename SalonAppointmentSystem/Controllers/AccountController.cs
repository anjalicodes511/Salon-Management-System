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
        [ValidateAntiForgeryToken]
        public ActionResult Signup(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Enter Valid Details";
                return View(model);
            }
            try
            {
                model.Email = model.Email.Trim().ToLower();

                var CheckParams = new DynamicParameters();
                CheckParams.Add("@Email", model.Email);

                var existingUser = DapperORM.ReturnSingle<CustomerModel>("GetCustomer",CheckParams);

                if(existingUser != null)
                {
                    ModelState.AddModelError("", "Email Already Exists");
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
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                return View(model);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Enter Valid Detail";
                return View(user);
            }
            try
            {
                user.Email = user.Email.Trim().ToLower();

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Email", user.Email);

                var result = DapperORM.ReturnSingle<CustomerModel>("GetCustomer", dp);

                if(result == null || new PasswordHasher().VerifyHashedPassword(result.Password,user.Password) != PasswordVerificationResult.Success)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(user);
                }

                if (!result.IsActive)
                {
                    ModelState.AddModelError("", "Account is inactive.");
                    return View(user);
                }

                //Session
                Session["Username"] = result.FullName;
                Session["Role"] = result.Role;
                Session["UserId"] = result.CustomerId;

                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}


