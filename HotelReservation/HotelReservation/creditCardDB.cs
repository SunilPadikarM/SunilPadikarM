using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CreditCardDetails.Models
{
    public class creditCardDB : DbContext
    {

        public DbSet<CardDetails> cardDetails { get; set; }
    }
}