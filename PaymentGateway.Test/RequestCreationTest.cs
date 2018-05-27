using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;
using Operators = PaymentGateway.Model.Entity.Operators;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Test
{
    /// <summary>
    /// Unit test class for CRUD operations on the database.
    /// </summary>
    public class RequestCreationTest
    {
        /// <summary>
        /// Class constructor to initialize some test parameters.
        /// </summary>
        public RequestCreationTest()
        {
            Store = StoreRepository.GetStore(1);
            Card = PersonRepository.GetCard("123456");
            Items = new AntiFraud.Item[]
            {
                new AntiFraud.Item("1", "Item1", 10, 2),
                new AntiFraud.Item("2", "Item2", 20, 3),
                new AntiFraud.Item("3", "Item3", 30, 4),
                new AntiFraud.Item("4", "Item4", 40, 5)
            };
        }

        private const int _installments = 1;
        private const string _orderId = "#123456789#";

        private decimal Amount => Items.Sum(i => i.ItemValue * i.Qty);
        private Store Store { get; set; }
        private Operators.CreditCard Card { get; set; }
        private IEnumerable<AntiFraud.Item> Items { get; set; }


        [Fact]
        public void successfullyCreatesOperatorRequest()
        {
            // Setup
            var transaction = new Operators.Transaction(Amount, Card, _installments);
            var request = new Operators.Request(transaction, _orderId);

            // Assertions
            Assert.Equal("Carlos", request.Customer.Name);
            Assert.Equal(_orderId, request.OrderId);
            Assert.Equal(400, request.Transaction.AmountInCents);
            Assert.Equal(_installments, request.Transaction.InstallmentCount);
        }

        [Fact]
        public void successfullyCreatesAntiFraudRequest()
        {
            // Setup
            var transaction = new Operators.Transaction(Amount, Card, _installments);
            var order = new AntiFraud.Order(Store, Items, transaction, _orderId);

            var orders = new List<AntiFraud.Order>() { order };
            var request = new AntiFraud.Request(Store.AntiFraudInfo.ApiKey, Store.AntiFraudInfo.LoginToken, orders, "BRA");

            // Assertions
            Assert.Equal("Carlos Key", request.ApiKey);
            Assert.Equal("Carlos Token", request.LoginToken);
            Assert.NotNull(request.Orders.First().ShippingData);
            Assert.NotNull(request.Orders.First().BillingData);
        }

        [Fact]
        public void storeWithUnknownOperatorRaisesException()
        {
            var gatewayWithInvalidOperator = new Gateway(1);
            var invalidOperatorIndex = 2;
            Assert.Throws<NotImplementedException>(() => {
                gatewayWithInvalidOperator.MakeRequest(Items, _installments, Card, invalidOperatorIndex);
                }
            );
        }

        [Fact]
        public void storeInvalidOperatorIndexRaisesException()
        {
            var gatewayWithInvalidOperator = new Gateway(1);
            Assert.Throws<InvalidOperationException>(() => {
                gatewayWithInvalidOperator.MakeRequest(Items, _installments, Card, 111);
            });
        }


    } //class
} //namespace
