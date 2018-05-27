using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

using Operators = PaymentGateway.Model.Entity.Operators;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Model.Business
{
    /// <summary>
    /// Mocks the behaviour of the operators and antifraud APIs.
    /// </summary>
    public static class ApiResponseMock
    {
        /// <summary>
        /// Mocks the response of the Stone API and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static Operators.Response MockStoneResponse(Operators.Request request)
        {
            return new Operators.Response();
        }


        /// <summary>
        /// Mocks the response of the Cielo API and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static Operators.Response MockCieloResponse(string jsonRequest)
        {
            return new Operators.Response();
        }


        /// <summary>
        /// Mocks the response of the Cielo API and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static AntiFraud.Response MockClearSaleResponse(IEnumerable<AntiFraud.Order> orders, string jsonRequest)
        {
            // TODO: Mock this!!!
            var status = "APA"; // approved
            var score = 50.0m;


            var orderStatus = orders.Select(o => new AntiFraud.OrderStatus(o.ID, status, score));
            return new AntiFraud.Response(orderStatus);
        }


    } //class
} //namespace
