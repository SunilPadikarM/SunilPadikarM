using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelReservation.Models;
using HotelReservation.Controllers;

namespace HotelReservation.Controllers
{
    public class HomeController : Controller
    {
        private Reservation_Room db = new Reservation_Room();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "WELCOME TO DAN'S HAPPY HOTEL";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "contact us here";

            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            List<Room> rooms = new List<Room>();
            List<Reservation> reservations = null;
            if (form.Get("CheckInDate") != null)
            {
                Reservation reservation = new Reservation();
                reservation.CheckInDate = Convert.ToDateTime(form.Get("CheckInDate"));
                reservation.CheckOutDate = Convert.ToDateTime(form.Get("CheckOutDate"));
                reservation.NoOfGuests = Convert.ToInt32(form.Get("NoOfGuests"));
                reservation.NoOfRooms = Convert.ToInt32(form.Get("NoOfRooms"));

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

                foreach (Reservation res in reservations)
                {
                    foreach (Room rm in rooms.ToList())
                    {
                        if (rm.RoomNumber == res.RoomNumber)
                        {
                            rooms.Remove(rm);
                        }
                    }
                }
                Session["CheckInDate"] = dateTime1;
                Session["CheckOutDate"] = dateTime;
                Session["NoOfGuests"] = reservation.NoOfGuests;
            }
            ViewBag.Rooms = rooms;
            ViewBag.RoomsCount = rooms.Count;
            return View();
        }

    }
}