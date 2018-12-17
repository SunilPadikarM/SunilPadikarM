using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace HotelReservation.Models
{
    public class Feedback
    {
        
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

       
        public int? Answer { get; set; }
        [StringLength(500)]
        
        public string Comment { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(250)]

        public string Email { get; set; }

  
    }
}