using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelReservation.Models;

namespace HotelReservation.Controllers
{
    public class ReservationsController : Controller
    {
        private Reservation_Room db = new Reservation_Room();


        public ActionResult TransactionSummary()
        {
            if(null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }

            List<TransactionSummary> transactionSummaries = new List<TransactionSummary>();
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<Reservation> reservations = db.Reservations.Where(u => u.ID == UserId).ToList();

            Transaction.TrxnWebServiceClient transaction = new Transaction.TrxnWebServiceClient();
            foreach(Reservation res in reservations)
            {
                string summary = transaction.getTransaction(res.ReservationNumber);
                TransactionSummary transactionSummary = new TransactionSummary();
                String[] transactionArray = summary.Split(',');
                transactionSummary.TransactionID = Convert.ToInt32(transactionArray[0]);
                transactionSummary.NameOnCard = transactionArray[1];
                transactionSummary.CardNumber = transactionArray[2];
                transactionSummary.CardType = transactionArray[3];
                transactionSummary.qty = Convert.ToInt32(transactionArray[4]);
                transactionSummary.Price= Convert.ToDouble(transactionArray[5]);
                transactionSummary.TotalPrice = Convert.ToDouble(transactionArray[6]);
                transactionSummaries.Add(transactionSummary);
            }


            return View(transactionSummaries);
        }


        public ActionResult bookRoom(Room room)
        {
            return View("CreditCardController");
        }

        public ActionResult SearchRooms(Reservation reservation)
        {
            List<Room> rooms = new List<Room>();
            List<Reservation> reservations = null;
            if (reservation.CheckInDate != null)
            {
                DateValidation.ReservationDateValidate dateValidation = new DateValidation.ReservationDateValidate();
                string validationString = dateValidation.IsValid(reservation);
                if (!validationString.Equals(""))
                {
                    ViewBag.Message = validationString;
                    return View(rooms);
                }
                else
                {
                    ViewBag.Message = "";
                }

                rooms = db.Rooms.ToList();
                String date = reservation.CheckOutDate.Value.ToString("yyyy/MM/dd");
                String date1 = reservation.CheckInDate.Value.ToString("yyyy/MM/dd");

                DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime dateTime1 = DateTime.ParseExact(date1, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //reservations = db.Reservations.Where(u => u.CheckInDate >= dateTime || u.CheckOutDate <= dateTime1 && u.RoomNumber == u.Room.RoomNumber).Distinct().ToList();

                reservations = db.Reservations.Where(u => u.CheckInDate <= dateTime && u.CheckOutDate >= dateTime1).ToList();
                
                foreach(Reservation res in reservations)
                {
                    foreach(Room rm in rooms.ToList())
                    {
                        if(rm.RoomNumber == res.RoomNumber)
                        {
                            rooms.Remove(rm);
                        }
                    }
                }
                Session["CheckInDate"] = dateTime1;
                Session["CheckOutDate"] = dateTime;
                Session["NoOfGuests"] = reservation.NoOfGuests;
            }

            if (reservations != null)
            {
                return View(rooms);
            }
            return View(rooms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

