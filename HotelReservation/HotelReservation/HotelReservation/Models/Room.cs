namespace HotelReservation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]

        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }

        [Display(Name = "Price")]
        public int? Price { get; set; }

        [StringLength(10)]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
