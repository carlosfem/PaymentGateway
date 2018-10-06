using System;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using Operators = PaymentGateway.Model.Entity.Operators;


namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Order
    {
        /// <summary>
        /// Creates an order based on a store's transaction and items being purchased.
        /// </summary>
        /// <param name="store">Store requesting the transaction</param>
        /// <param name="items">Items being purchased</param>
        /// <param name="transaction">Transaction being requested</param>
        /// <param name="currency">Base currency for the transaction</param>
        public Order(Store store, IEnumerable<Item> items, Operators.Transaction transaction, string orderId, string currency = "BRL")
        {
            var card = transaction.CreditCard;
            var payment = new Payment(transaction);

            ID       = orderId;
            Date     = DateTime.Today;
            Email    = card.Holder.Email;
            IP       = store.IpAddress;
            Currency = currency;

            Items        = items;
            Payments     = new List<Payment>() { payment };
            BillingData  = card.Holder;
            ShippingData = store.Owner;

            // Optional
            TotalShipping = 0;
            Obs = string.Empty;
            Status = "N";
            Reanalysis = false;
            CustomFields = new List<CustomFields>();
        }


        // Mandatory fields
        public string ID { get; private set; }
        public DateTime Date { get; private set; }
        public string Email { get; private set; }
        public string IP { get; private set; }
        public string Currency { get; private set; }
        public string Origin { get; private set; }

        /// <summary>
        /// Payments associated to the order.
        /// </summary>
        public IEnumerable<Payment> Payments { get; private set; }

        /// <summary>
        /// Information on the seller.
        /// </summary>
        public Person ShippingData { get; private set; }

        /// <summary>
        /// Information on the buyer.
        /// </summary>
        public Person BillingData { get; private set; }

        /// <summary>
        /// Order items.
        /// </summary>
        public IEnumerable<Item> Items { get; private set; }


        // Optional fields
        public string Obs { get; set; }
        public string Status { get; set; }
        public bool Reanalysis { get; set; }
        public IEnumerable<CustomFields> CustomFields { get; set; }

        /// <summary>
        /// Total shipping value.
        /// </summary>
        public decimal TotalShipping { get; set; }

        /// <summary>
        /// Sum of items values.
        /// </summary>
        public decimal TotalItems => Items.Sum(i => i.ItemValue * i.Qty);

        /// <summary>
        /// Total order value.
        /// </summary>
        public decimal TotalOrder => TotalShipping + TotalItems;


    } //class
} //namespace
