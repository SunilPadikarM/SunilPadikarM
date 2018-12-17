using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservation.Controllers
{
    public class SingupController : Controller
    {
        Models.SignUp singup = new Models.SignUp();

        private static Models.SignUp SignUp()
        {
            throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UploadData(Models.Customer customer)
        {
            singup.Customers.Add(customer);
            singup.SaveChanges();
            return Json("Signed up Successfully", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AuthenticateLogin(Models.Customer customer)
        {
        
            return Json("Logged in Successfully " , JsonRequestBehavior.AllowGet);
        }
    }
}