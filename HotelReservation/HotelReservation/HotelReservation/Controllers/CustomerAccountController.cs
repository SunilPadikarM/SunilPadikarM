using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelReservation.Models;

namespace HotelReservation.Controllers
{
    public class CustomerAccountController : Controller
    {
        
        public ActionResult Singup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Singup(CustomerAccount cusAccount)
        {
            if (ModelState.IsValid)
            {
                using (CustomerData db = new CustomerData())
                {
                    var usr = db.customeraccount.Where(u => u.Email == cusAccount.Email);
                    if(null != usr && usr.Count() != 0)
                    {
                        ViewBag.Message = "Email already Exists, Please try different email!";
                        return View();
                    }
                    db.customeraccount.Add(cusAccount);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = cusAccount.FirstName + " " + cusAccount.LastName + " Successfully Registered";
            }
            TempData["SingUpMessage"] = "Signed Up Successfully, Please Login!";
            return RedirectToAction("Login");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            using(CustomerData db = new CustomerData())
            {
                try { 
                var usr = db.customeraccount.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if(null != usr)
                {
                    Session["UserId"] = usr.ID.ToString();
                    Session["User Name"] = usr.FirstName + " " + usr.LastName;
                        ViewBag.Message = "";
                    if(Session["CheckInDate"] != null && Session["CheckOutDate"] != null)
                    {
                            return RedirectToAction("../CardDetails/Index");
                        }
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.Message = "User Name or Password is wrong!";
                }
                } catch(Exception ex)
                {
                    ModelState.AddModelError("", "User Name or Password is wrong!");
                    throw ex;
                    
                }
                return View();
            }
        }

        public ActionResult EditCustomer()
        {
            if(null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            int ID = Convert.ToInt32(Session["UserId"]);
            CustomerData cusdata = new CustomerData();
            if(null != Request.Params.Get("FirstName")) { 
            CustomerAccount customerAccount = new CustomerAccount();
            customerAccount.addrses1 = Request.Params.Get("addrses1");
            customerAccount.FirstName = Request.Params.Get("FirstName");
            customerAccount.LastName = Request.Params.Get("LastName");
            customerAccount.Phone = Request.Params.Get("Phone");
            customerAccount.City = Request.Params.Get("City");
            customerAccount.Postal = Request.Params.Get("Postal");
            customerAccount.Province = Request.Params.Get("Province");
            customerAccount.Password = Request.Params.Get("Password");
            customerAccount.Email = Request.Params.Get("Email");
            customerAccount.Country = Request.Params.Get("Country");
            cusdata.customeraccount.Add(customerAccount);
            cusdata.SaveChanges();
            }
            CustomerAccount customer = cusdata.customeraccount.FirstOrDefault(u => u.ID == ID);
            return View(customer);
        }
        public ActionResult LoggedIn()
        {
            if(Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        
            public ActionResult LogOff()
        {
            if (Session["UserId"] != null)
            {
                Session.Clear();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}