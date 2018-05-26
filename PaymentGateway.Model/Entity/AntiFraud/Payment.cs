using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Payment
    {
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


    } //class
} //namespace
