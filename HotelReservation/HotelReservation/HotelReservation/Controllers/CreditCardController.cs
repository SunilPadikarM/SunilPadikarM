using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelReservation.Models;

namespace HotelReservation.Controllers
{
    public class CardDetailsController : Controller
    {
        private CustomerData db = new CustomerData();

        // GET: CardDetails
        public ActionResult Index()
        {
            if (Request.Params.Get("RoomNumber") != null)
            {
                Room room = new Room();
                room.RoomNumber = int.Parse(Request.Params.Get("RoomNumber"));
                room.RoomType = Request.Params.Get("RoomType");
                room.Price = int.Parse(Request.Params.Get("Price"));
                Session["room"] = room;
            }
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            int id = Convert.ToInt32(Session["UserId"]);
            return View(db.cardDetails.Where(u => u.ID == id).ToList());
        }

        public ActionResult TransactionSummary()
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            TransactionSummary transactionSummary = new TransactionSummary();
            transactionSummary.TransactionID = Convert.ToInt32(Request.Params.Get("TransactionID"));
            transactionSummary.CardNumber = Request.Params.Get("CardNumber");
            transactionSummary.NameOnCard = Request.Params.Get("NameOnCard");;
            transactionSummary.Price = double.Parse(Request.Params.Get("Price").ToString());
            transactionSummary.TotalPrice =Convert.ToDouble(Request.Params.Get("TotalPrice"));
            transactionSummary.qty = Convert.ToInt32(Request.Params.Get("qty"));
            transactionSummary.ExpDate = Request.Params.Get("ExpDate");
            transactionSummary.CardType = Request.Params.Get("CardType");
            return View(transactionSummary);
        }

        public ActionResult MakePayment(CardDetail cardDetail)
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            Room room = (Room)Session["room"];

            Reservation reservation = new Reservation();
            reservation.CheckInDate = (DateTime)Session["CheckInDate"];
            reservation.CheckOutDate = (DateTime)Session["CheckOutDate"];
            reservation.RoomNumber = room.RoomNumber;
            reservation.NoOfRooms = 1;
            reservation.NoOfGuests = (int)Session["NoOfGuests"];
            reservation.ID = Convert.ToInt32(Session["UserId"]);
            long reservid = db.reservations.LongCount(); //!= 1 ? db.reservations.Max(u => u.ReservationNumber) : 1;
            reservation.ReservationNumber = Convert.ToInt16(++reservid);
            db.reservations.Add(reservation);
            db.SaveChanges();
            TransactionSummary transactionSummary = new TransactionSummary();
            transactionSummary.TransactionID = reservation.ReservationNumber;
            transactionSummary.CardNumber = cardDetail.CardNumber;
            transactionSummary.NameOnCard= cardDetail.NameOnCard;
            transactionSummary.Price = double.Parse(room.Price.ToString());
            transactionSummary.qty = Convert.ToInt32(((DateTime)reservation.CheckOutDate - (DateTime)reservation.CheckInDate).TotalDays);
            transactionSummary.TotalPrice = (transactionSummary.qty * transactionSummary.Price);
            transactionSummary.ExpDate = cardDetail.ExpDate;
            transactionSummary.CardType = cardDetail.CardType.ToString();

            Transaction.TrxnWebServiceClient webservice = new Transaction.TrxnWebServiceClient();
            webservice.createTransaction(transactionSummary.TransactionID, transactionSummary.NameOnCard, transactionSummary.CardNumber, transactionSummary.Price, transactionSummary.qty, transactionSummary.TotalPrice, transactionSummary.ExpDate, transactionSummary.CardType);
            return RedirectToAction("TransactionSummary",transactionSummary);
        }

        // GET: CardDetails/Create
        public ActionResult Create()
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            return View();
        }

        // POST: CardDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardNumber,CardType,NameOnCard,ExpDate,ID")] CardDetail cardDetail)
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            cardDetail.ID = Convert.ToInt32(Session["UserId"]);
            if (ModelState.IsValid)
            {
                db.cardDetails.Add(cardDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cardDetail);
        }

        // GET: CardDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardDetail cardDetail = db.cardDetails.Find(id);
            if (cardDetail == null)
            {
                return HttpNotFound();
            }
            return View(cardDetail);
        }

        // POST: CardDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardNumber,CardType,NameOnCard,ExpDate,ID")] CardDetail cardDetail)
        {
            cardDetail.ID = Convert.ToInt32(Session["UserId"]);
            if (ModelState.IsValid)
            {
                db.Entry(cardDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cardDetail);
        }

        // GET: CardDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (null == Session["UserId"])
            {
                return RedirectToAction("../CustomerAccount/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardDetail cardDetail = db.cardDetails.Find(id);
            if (cardDetail == null)
            {
                return HttpNotFound();
            }
            return View(cardDetail);
        }

        // POST: CardDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CardDetail cardDetail = db.cardDetails.Find(id);
            db.cardDetails.Remove(cardDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
