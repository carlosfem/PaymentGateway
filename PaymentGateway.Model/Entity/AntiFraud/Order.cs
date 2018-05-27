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
        public Order(Store store, IEnumerable<Item> items, Operators.Transaction transaction, string orderId)
        {
            var card = transaction.CreditCard;
            var payment = new Payment(transaction);

            ID       = orderId;
            Date     = DateTime.Today;
            Email    = card.Holder.Email;
            IP       = store.IpAddress;
            Currency = "BRL"; // Assuming that all transactions are performed in native currency (easy to extend)

            Payments     = new List<Payment>() { payment };
            ShippingData = store.Owner;
            BillingData  = card.Holder;
            Items        = items;
        }

        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Order(string id, DateTime date, string email, string ip, string currency,
            IEnumerable<Payment> payments, Person seller, Person buyer, IEnumerable<Item> items)
        {
            // Mandatory
            ID       = id;
            Date     = date;
            Email    = email;
            IP       = ip;
            Currency = currency;

            Payments     = payments;
            ShippingData = seller;
            BillingData  = buyer;
            Items        = items;

            // Optional
            Obs = string.Empty;
            Status = "N";
            Reanalysis = false;
            CustomFields = new List<CustomFields>();
        }

        public string ID { get; private set; }
        public DateTime Date { get; private set; }
        public string Email { get; private set; }
        public string IP { get; private set; }
        public string Currency { get; private set; }
        public string Origin { get; private set; }

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


    } //class
} //namespace
