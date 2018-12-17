using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using HotelReservation.Models;

namespace HotelReservation
{
    public class CardValidation : ValidationAttribute
    {
            private const string MasterCard = "Master Card";
            private const string AmericanExpress = "American Express";
            private const string Visa = "Visa";

        protected override ValidationResult IsValid(object value,
                        ValidationContext validationContext)
        {
            CardDetail cardDetail = (CardDetail)validationContext.ObjectInstance;
                string cardNumber = (string) value ;
                string cardType = cardDetail.CardType;
            if(null != cardDetail && null != cardDetail.CardNumber && null != cardDetail.CardType) { 
                if (cardType.Equals(MasterCard) &&  !((cardNumber.StartsWith("51") || cardNumber.StartsWith("52") || cardNumber.StartsWith("53") || cardNumber.StartsWith("54") || cardNumber.StartsWith("55")) || cardNumber.Length == 16))
                {
                    return new ValidationResult(MasterCard + " Card Number Should start with 51,52,53,54,55 and Should be of lenght 16");

                }
                else if (cardType.Equals(AmericanExpress) &&  !((cardNumber.StartsWith("34") || cardNumber.StartsWith("37")) && cardNumber.Length == 15))
                {
                    return new ValidationResult(AmericanExpress + " Card Number Should start with 37/34 and Should be of lenght 15");
                }
                else if (cardType.Equals(Visa) && !(cardNumber.StartsWith("4") && cardNumber.Length == 16))
                {
                    return new ValidationResult(Visa + " Card Number Should start with 4 and Should be of lenght 16");
                }
            }
            return ValidationResult.Success;
            }
        }
    }