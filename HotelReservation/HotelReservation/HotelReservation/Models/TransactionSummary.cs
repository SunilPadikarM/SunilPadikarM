namespace HotelReservation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class TransactionSummary
    {
        [Key]
        public int TransactionID { get; set; }
        public double Price { get; set; }
        public int qty { get; set; }
        public double TotalPrice { get; set; }

        public string CardNumber { get; set; }

        public string CardType { get; set; }

        public string NameOnCard { get; set; }

        public string ExpDate { get; set; }
    }

}
