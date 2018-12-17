namespace HotelReservation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReservationNumber { get; set; }
        [Required(ErrorMessage = "CheckOut Date is Required!")]

        [Column(TypeName = "date")]
        public DateTime? CheckInDate { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "CheckOut Date is Required!")]
        public DateTime? CheckOutDate { get; set; }

        public int? NoOfGuests { get; set; }

        public int? NoOfRooms { get; set; }

        public int ID { get; set; }

        public int RoomNumber { get; set; }

        public virtual Room Room { get; set; }
    }
}
