using System;
using System.ComponentModel;


namespace PaymentGateway.Model.Entity.Operators
{
    public class Transaction
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public Transaction(decimal amount, CreditCard card, int installments)
        {
            AmountInCents    = amount;
            CreditCard       = card;
            InstallmentCount = installments;
        }

        public decimal AmountInCents { get; private set; }
        public CreditCard CreditCard { get; private set; }
        public int InstallmentCount { get; private set; }


    } //class
} //namsspace
