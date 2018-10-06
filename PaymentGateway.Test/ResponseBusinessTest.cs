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
            Test = new TestCase();
        }
        private TestCase Test { get; set; }


        [Fact]
        public void validRequestIsAuthorized()
        {
            var transaction = new Operators.Transaction(Test.Amount, Test.Card, Test.Installments);
            var order = new AntiFraud.Order(Test.Store, Test.Items, transaction, Test.OrderId);

            // Antifraud step
            var orders = new List<AntiFraud.Order>() { order };
            var req1 = new AntiFraud.Request(Test.Store.AntiFraudInfo.ApiKey, Test.Store.AntiFraudInfo.LoginToken, orders, "BRA");
            var jsonRequest = JsonConvert.SerializeObject(req1);
            var responseAF = ApiResponseMock.MockClearSaleResponse(orders, jsonRequest);
            var allValid = responseAF.All(r => r.AllValid);

            // Operator step
            var req2 = new Operators.Request(transaction, Test.OrderId);
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
            var transaction = new Operators.Transaction(Test.Amount, fraudulentCard, Test.Installments);
            var order = new AntiFraud.Order(Test.Store, Test.Items, transaction, Test.OrderId);

            // Antifraud step
            var orders = new List<AntiFraud.Order>() { order };
            var request = new AntiFraud.Request(Test.Store.AntiFraudInfo.ApiKey, Test.Store.AntiFraudInfo.LoginToken, orders, "BRA");
            var jsonRequest = JsonConvert.SerializeObject(request);
            var response = ApiResponseMock.MockClearSaleResponse(orders, jsonRequest);
            var allValid = response.All(r => r.AllValid);

            Assert.False(allValid);
        }

        [Fact]
        public void poorCustomerIsDenied()
        {
            var poorCard = PersonRepository.GetCard("963258");
            var transaction = new Operators.Transaction(Test.Amount, poorCard, Test.Installments);
            var order = new AntiFraud.Order(Test.Store, Test.Items, transaction, Test.OrderId);

            // Operator step
            var request = new Operators.Request(transaction, Test.OrderId);
            var response = RequestManager.MakeCieloRequest(request);

            Assert.Equal(3, response.Status);
            Assert.Equal("4", response.ReturnCode);
        }


    } //class
} //namespace