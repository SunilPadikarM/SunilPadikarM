using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using HotelReservation.Models;

namespace DateValidation
{
    class ReservationDateValidate
    {
        public string IsValid(Reservation reservation)
        {

            DateTime checkinDate = (DateTime) reservation.CheckInDate;
            DateTime checkoutDate = (DateTime) reservation.CheckOutDate;

            int result = DateTime.Compare(checkinDate, checkoutDate);
            
            if(checkinDate < DateTime.Now.Date)
            {
                return "Checkin Date should greater than equal to current date";
            }

            if((DateTime)checkoutDate <= checkinDate)
            {
                return "CheckOut Date should be greater than Checkin Date";
            }

            return "";
        }
    }
}
