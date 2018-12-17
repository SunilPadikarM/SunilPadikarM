namespace HotelReservation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public int ID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string addrses1 { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string country { get; set; }

        public string postal { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        [StringLength(10)]
        public string password { get; set; }
    }
}
