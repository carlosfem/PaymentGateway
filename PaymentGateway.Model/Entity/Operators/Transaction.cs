using System;
using System.Data;
using Newtonsoft.Json;
using System.ComponentModel;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


namespace PaymentGateway.Model.Entity.Operators
{
    [JsonObject("Payment")]
    public class Transaction
    {
        /// <summary>
        /// Standard constructor, required for Json deserialization.
        /// </summary>
        public Transaction()
        {

        }

        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public Transaction(decimal amount, CreditCard card, int installments)
        {
            AmountInCents    = amount;
            Type             = "CreditCard";
            CreditCard       = card;
            InstallmentCount = installments;
        }


        // Optional (only for stored transactions)
        [JsonIgnore()]
        public bool Authorized { get; set; }

        [JsonIgnore()]
        public string Message { get; set; }


        // Mandatory
        [JsonProperty("Amount")]
        public decimal AmountInCents { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("CreditCard")]
        public CreditCard CreditCard { get; set; }

        [JsonProperty("Installments")]
        public int InstallmentCount { get; set; }


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
