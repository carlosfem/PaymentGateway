using System;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Repository;
using Operators = PaymentGateway.Model.Entity.Operators;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Test
{
    /// <summary>
    /// Used for testing purposes.
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Class constructor to initialize some test parameters.
        /// </summary>
        public TestCase()
        {
            Installments = 1;
            OrderId = "#123456789#";

            Store = StoreRepository.GetStore(1);
            Card  = PersonRepository.GetCard("123456");
            Items = new AntiFraud.Item[]
            {
                new AntiFraud.Item("1", "Item1", 10, 2),
                new AntiFraud.Item("2", "Item2", 20, 3),
                new AntiFraud.Item("3", "Item3", 30, 4),
                new AntiFraud.Item("4", "Item4", 40, 5)
            };
        }

        public Store Store { get; set; }
        public Operators.CreditCard Card { get; set; }
        public IEnumerable<AntiFraud.Item> Items { get; set; }

        public int Installments { get; set; }
        public string OrderId { get; set; }
        public decimal Amount => Items.Sum(i => i.ItemValue * i.Qty);


    } //class
} //namespace
