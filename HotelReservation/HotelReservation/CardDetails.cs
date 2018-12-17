using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardDetails.Models
{
    [Table("CardDetails")]
    public class CardDetails
    {
       [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Card type required.")]
        public string cardType { get; set; }

        [Required(ErrorMessage = "Name on the card required.")]
        public string NameOnCard { get; set; }
      
        [Required(ErrorMessage ="Card Number required.")]
        public long creditCardNumber { get; set; }


        [Required(ErrorMessage = "Expire Date required.")]
        public string ExpDate { get; set; }

    }
}