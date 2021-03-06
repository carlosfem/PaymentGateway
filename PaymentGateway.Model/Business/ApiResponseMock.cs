﻿using System;
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
        // Only these 2 cards will be denied in the mocking process.
        private const string POOR_GUY_NUMBER = "963258";
        private const string FRAUD_GUY_NUMBER = "987654";

        /// <summary>
        /// Mocks the response of the Stone API and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static Operators.Response MockStoneResponse(Operators.Request request)
        {
            // The Stone API returns a regular c# object, could easily be parsed to the domain Response.
            var status = request.Transaction.CreditCard.CreditCardNumber == POOR_GUY_NUMBER
                ? Operators.ResponseStatus.Denied
                : Operators.ResponseStatus.Approved;

            return new Operators.Response(request, status);
        }


        /// <summary>
        /// Mocks the response of the Cielo API and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static Operators.Response MockCieloResponse(string jsonRequest)
        {
            var request = JsonConvert.DeserializeObject<Operators.Request>(jsonRequest);
            var card = request.Transaction.CreditCard;

            // Approval mock
            var status = card.CreditCardNumber == POOR_GUY_NUMBER ? 3 : 1;
            var code = card.CreditCardNumber == POOR_GUY_NUMBER ? "4" : "0";
            var msg = card.CreditCardNumber == POOR_GUY_NUMBER ? "Transaction Denied!" : "Transaction Approved!";

            var mockedString =
                "{\"Request\":{\"Payment\":{\"Amount\":400.0,\"Type\":"
                + "\"CreditCard\",\"CreditCard\":{\"CardNumber\":" 
                + $"\"{card.CreditCardNumber}\"," 
                + $"\"Brand\":\"{card.CreditCardBrandAsString}\"," 
                + $"\"Holder\":\"{card.HolderName}\"," 
                + $"\"ExpirationDate\":\"{card.ExpirationAsString}\"," 
                + $"\"SecurityCode\":\"{card.SecurityCode}" + "\"},\"Installments\":1}," 
                + $"\"MerchantOrderId\":\"{request.OrderId}\"," + "\"Customer\":{\"Name\":" 
                + $"\"{card.HolderName}" + "\"}}"
                + $",\"Status\":{status},\"ReturnCode\":\"{code}\",\"ReturnMessage\":\"{msg}" + "\"}";

            var response = JsonConvert.DeserializeObject<Operators.Response>(mockedString);

            return response;
        }


        /// <summary>
        /// Mocks the response of the anti-fraud system and converts it to the domain specific response.
        /// </summary>
        /// <returns>Domain specific Response object</returns>
        public static IEnumerable<AntiFraud.Response> MockClearSaleResponse(IEnumerable<AntiFraud.Order> orders, string jsonRequest)
        {
            var responses = orders.Select(order =>
            {
                var cardNumber = order.Payments.First().CardNumber;
                var status = cardNumber == FRAUD_GUY_NUMBER ? "FRD" : "APA";
                var score = cardNumber == FRAUD_GUY_NUMBER ? 0 : 100.0;

                var mockedString = "{\"Orders\":[{\"IdOrder\":"
                    + $"\"{orders.First().ID}\",\"Status\":\"{status}\",\"Score\":{score}"
                    + "}]}";
                return JsonConvert.DeserializeObject<AntiFraud.Response>(mockedString);
            });

            return responses;
        }



    } //class
} //namespace
