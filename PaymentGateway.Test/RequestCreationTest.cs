using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.Business;
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
            Test = new TestCase();   
        }
        private TestCase Test { get; set; }


        [Fact]
        public void successfullyCreatesOperatorRequest()
        {
            // Setup
            var transaction = new Operators.Transaction(Test.Amount, Test.Card, Test.Installments);
            var request = new Operators.Request(transaction, Test.OrderId);

            // Assertions
            Assert.Equal("Carlos", request.Customer.Name);
            Assert.Equal(Test.OrderId, request.OrderId);
            Assert.Equal(400, request.Transaction.AmountInCents);
            Assert.Equal(Test.Installments, request.Transaction.InstallmentCount);
        }

        [Fact]
        public void successfullyCreatesAntiFraudRequest()
        {
            // Setup
            var transaction = new Operators.Transaction(Test.Amount, Test.Card, Test.Installments);
            var order = new AntiFraud.Order(Test.Store, Test.Items, transaction, Test.OrderId);

            var orders = new List<AntiFraud.Order>() { order };
            var request = new AntiFraud.Request(Test.Store.AntiFraudInfo.ApiKey, Test.Store.AntiFraudInfo.LoginToken, orders, "BRA");

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
                gatewayWithInvalidOperator.MakeRequest(Test.Items, Test.Installments, Test.Card, invalidOperatorIndex);
                }
            );
        }

        [Fact]
        public void storeInvalidOperatorIndexRaisesException()
        {
            var gatewayWithInvalidOperator = new Gateway(1);
            Assert.Throws<InvalidOperationException>(() => {
                gatewayWithInvalidOperator.MakeRequest(Test.Items, Test.Installments, Test.Card, 111);
            });
        }


    } //class
} //namespace
