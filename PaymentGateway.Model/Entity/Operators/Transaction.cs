using System;
using System.Data;
using System.ComponentModel;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


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

        // Mandatory
        public decimal AmountInCents { get; set; }
        public CreditCard CreditCard { get; set; }
        public int InstallmentCount { get; set; }

        // Optional (only for stored transactions)
        public bool Authorized { get; set; }
        public string Message { get; set; }


        /// <summary>
        /// Conversion from a DataRow into a Transaction instance.
        /// </summary>
        public static explicit operator Transaction(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Transaction not found!");

            var amount       = Helpers.ConvertFromDBVal<decimal>(row["AmountInCents"]);
            var cardNumber   = Helpers.ConvertFromDBVal<string>(row["CardNumber"]);
            var installments = Helpers.ConvertFromDBVal<int>(row["installments"]);
            var authorized   = Helpers.ConvertFromDBVal<bool>(row["Authorized"]);
            var message      = Helpers.ConvertFromDBVal<string>(row["Message"]);
            var card         = PersonRepository.GetCard(cardNumber);

            var transaction = new Transaction(amount, card, installments);
            transaction.Message = message;
            transaction.Authorized = authorized;
            return transaction;
        }


    } //class
} //namsspace
