namespace HotelReservation.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CardDetail
    {
        private const string SpecialChars = @"[^\d\:\!\@\#\$\%\^\*\+\?\\\/\<\>]*";
        private const string SpecialCharsMessage = "SpecialCharacters(:!@#$%^*+?/<>1234567890) not allowed!";

        [Key]
        [StringLength(19)]
        [Required(ErrorMessage = "Card Number is Required!")]
        [CardValidation]
        public string CardNumber { get; set; }

        [StringLength(16)]
        [Required(ErrorMessage = "Card Type is Required!")]
        public string CardType { get; set; }

        [StringLength(50)]
        [RegularExpression(SpecialChars, ErrorMessage = SpecialCharsMessage)]
        [Required(ErrorMessage = "Name On Card is Required!")]
        public string NameOnCard { get; set; }

        [StringLength(8)]
        [RegularExpression("^(0[1-9]|1[0-2])/20(1[6-9]|2[0-9]|3[0-1])$", ErrorMessage = "Expiry date should be between 2016 and 2031, The range of Month is between 01 and 12(MM/YYYY)")]
        [Required(ErrorMessage = "Expiry Date is Required!")]
        public string ExpDate { get; set; }

        public int? ID { get; set; }
    }
}
