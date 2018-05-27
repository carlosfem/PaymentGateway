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
        public Payment(Operators.Transaction transaction)
        {
            var card = transaction.CreditCard;

            Date   = DateTime.Today; // Credit card transactions are live
            Amount = transaction.AmountInCents * 100; // The AntiFraud.Payment amount is on currency units
            Type   = 1; // Only credit card transactions

            SetCardInfo(
                GetCardType(card.CreditCardBrand),
                card.CreditCardNumber,
                card.Holder.Name,
                card.ExpirationAsString
            );
        }

        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Payment(DateTime date, decimal amount, int type)
        {
            Date   = date;
            Amount = amount;
            Type   = type;
        }

        /// <summary>
        /// Sets the card information.
        /// </summary>
        public void SetCardInfo(int type, string number, string holderName, string expirationDate)
        {
            CardType           = type;
            CardNumber         = number;
            CardHolderName     = holderName;
            CardExpirationDate = expirationDate;
        }

        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public int Type { get; private set; }

        public int CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpirationDate { get; set; }

        public int PaymentTypeID { get; set; }
        public string CardBin { get; set; }

        /// <summary>
        /// Gets the card type according to the given card brand enum.
        /// </summary>
        private int GetCardType(CreditCardBrandEnum cardBrand)
        {
            switch (cardBrand.GetDescription())
            {
                case "MasterCard":
                    return 2;
                case "Visa":
                    return 3;
                case "HiperCard":
                    return 6;
                default:
                    return 4;
            }
        }


    } //class
} //namespace
