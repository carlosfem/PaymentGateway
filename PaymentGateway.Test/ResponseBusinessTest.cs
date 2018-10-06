using Xunit;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;
using Operators = PaymentGateway.Model.Entity.Operators;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Test
{
    public class ResponseBusinessTest
    {
        /// <summary>
        /// Class constructor to initialize some test parameters.
        /// </summary>
        public ResponseBusinessTest()
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
        public void validRequestIsAuthorized()
        {
            var transaction = new Operators.Transaction(Amount, Card, _installments);
            var order = new AntiFraud.Order(Store, Items, transaction, _orderId);

            // Antifraud step
            var orders = new List<AntiFraud.Order>() { order };
            var req1 = new AntiFraud.Request(Store.AntiFraudInfo.ApiKey, Store.AntiFraudInfo.LoginToken, orders, "BRA");
            var jsonRequest = JsonConvert.SerializeObject(req1);
            var responseAF = ApiResponseMock.MockClearSaleResponse(orders, jsonRequest);
            var allValid = responseAF.All(r => r.AllValid);

            // Operator step
            var req2 = new Operators.Request(transaction, _orderId);
            var responseOP = RequestManager.MakeCieloRequest(req2);

            // Assertions
            Assert.True(allValid);
            Assert.Equal(1, responseOP.Status);
            Assert.Equal("0", responseOP.ReturnCode);
        }

        [Fact]
        public void fraudulentCustomerIsDenied()
        {
            var fraudulentCard = PersonRepository.GetCard("987654");
            var transaction = new Operators.Transaction(Amount, fraudulentCard, _installments);
            var order = new AntiFraud.Order(Store, Items, transaction, _orderId);

            // Antifraud step
            var orders = new List<AntiFraud.Order>() { order };
            var request = new AntiFraud.Request(Store.AntiFraudInfo.ApiKey, Store.AntiFraudInfo.LoginToken, orders, "BRA");
            var jsonRequest = JsonConvert.SerializeObject(request);
            var response = ApiResponseMock.MockClearSaleResponse(orders, jsonRequest);
            var allValid = response.All(r => r.AllValid);

            Assert.False(allValid);
        }

        [Fact]
        public void poorCustomerIsDenied()
        {
            var poorCard = PersonRepository.GetCard("963258");
            var transaction = new Operators.Transaction(Amount, poorCard, _installments);
            var order = new AntiFraud.Order(Store, Items, transaction, _orderId);

            // Operator step
            var request = new Operators.Request(transaction, _orderId);
            var response = RequestManager.MakeCieloRequest(request);

            Assert.Equal(3, response.Status);
            Assert.Equal("4", response.ReturnCode);
        }


    } //class
} //namespace