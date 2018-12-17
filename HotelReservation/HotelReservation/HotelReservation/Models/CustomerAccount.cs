using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using ValidationOfPostalCode;

namespace HotelReservation.Models
{
    [Table("Customer")]
    public class CustomerAccount
    {
        private const string SpecialChars = @"[^\d\:\!\@\#\$\%\^\*\+\?\\\/\<\>]*";
        private const string SpecialCharsMessage = "SpecialCharacters(:!@#$%^*+?/<>1234567890) not allowed!";

        [Key]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required!")]
        [RegularExpression(SpecialChars, ErrorMessage = SpecialCharsMessage)]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required!")]
        [RegularExpression(SpecialChars, ErrorMessage = SpecialCharsMessage)]
        public String LastName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is Required!")]
        public String addrses1 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is Required!")]
        [RegularExpression(SpecialChars, ErrorMessage = SpecialCharsMessage)]
        public string City { get; set; }
    
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is Required!")]
        [RegularExpression(@"[^\d\:\!\@\#\$\%\^\*\+\?\\\/\<\>]*", ErrorMessage = SpecialCharsMessage)]
        public string Province { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal")]
        [Required(ErrorMessage = "Postal is Required!")]
        [PostalValidate]
        public String Postal { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone Name is Required!")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public String Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public String Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required!")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        
        /*[Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm Password does not match!")]
        [DataType(DataType.Password)]
        public String ComparedPassword { get; set; }*/
    }
}
