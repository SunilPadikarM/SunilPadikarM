using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditCardDetails.Models;
using HotelReservation.Models;
using HotelReservation.Transaction;
namespace CreditCardDetails.Controllers
{
    public class CreditCardController : Controller
    {
        // GET: CreditCard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreditCardInformation()
        {
            Room room = new Room();
            if (Request.Params.Get("RoomNumber") != null) { 
            
            room.RoomNumber = int.Parse(Request.Params.Get("RoomNumber"));
            room.RoomType = Request.Params.Get("RoomType");
            room.Price = int.Parse(Request.Params.Get("Price"));
            }
            return View(room);
        }


        public ActionResult PayAndBook(CardDetails credit)
        {

           // TrxnWebServiceClient transaction = new TrxnWebServiceClient();
            //Object test = transaction.getTransaction(111);
            //transaction.createTransaction(credit.ID, credit.NameOnCard, credit.creditCardNumber.ToString(), credit.ID, 1, 100, credit.ExpDate, credit.cardType);
            //ServiceReference1 serviceReference1 = new ServiceReference1();
            return View();
        }
       /* public string CheckRadio(FormCollection f)
        {
            string card = f["CardType"].ToString();
            
        }*/

  
        
      /*  [HttpPost]
        public ActionResult CreditCardInformation(CardDetails cardDetails)
        {
            if (ModelState.IsValid)
            {
               
            }

            return View();
        }
        */
    }
}