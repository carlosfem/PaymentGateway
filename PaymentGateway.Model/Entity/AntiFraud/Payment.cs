using System;
using System.Collections.Generic;

using PaymentGateway.Model.Business;
using Operators = PaymentGateway.Model.Entity.Operators;


namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Payment
    {
        /// <summary>
        /// Creates a Payment object based on an operator's transaction.
        /// </summary>
        public Payment(Operators.Transaction transaction, int type = 1)
        {
            var card = transaction.CreditCard;

            Date   = DateTime.Today;                    // Credit card transactions are live
            Amount = transaction.AmountInCents * 100;   // The AntiFraud.Payment amount is on currency units
            Type   = type;                              // Only credit card transactions

            // Card info
            CardType           = (int)card.CreditCardBrand;
            CardNumber         = card.CreditCardNumber;
            CardHolderName     = card.Holder.Name;
            CardExpirationDate = card.ExpirationAsString;
        }


        // Mandatory fields
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public int Type { get; private set; }

        // Credit card info
        public int CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpirationDate { get; set; }

        // Optional fields
        public int PaymentTypeID { get; set; }
        public string CardBin { get; set; }


    } //class
} //namespace
