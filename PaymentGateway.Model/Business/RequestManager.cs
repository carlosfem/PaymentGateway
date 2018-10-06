using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Entity.Operators;
using PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Model.Business
{
    public static class RequestManager
    {
        /// <summary>
        /// Turns the domain objects into the Stone API specific objects 
        /// to mocks the request and subsequent response.
        /// </summary>
        /// <remarks>
        /// Here I mock the construction of the Stone API specific objects 
        /// instead of actually adding the API as a dependence.
        /// </remarks>
        public static Entity.Operators.Response MakeStoneRequest(Entity.Operators.Request request, string merchantId)
        {
            var card = request.Transaction.CreditCard;
            
            var transaction = new // Mock CreditCardTransaction
            {
                AmountInCents = request.Transaction.AmountInCents,
                CreditCard = new
                {
                    CreditCardBrand  = card.CreditCardBrand,
                    CreditCardNumber = card.CreditCardNumber,
                    ExpMonth         = card.Expiration.Month,
                    ExpYear          = card.Expiration.Year,
                    HolderName       = card.Holder.Name,
                    SecurityCode     = card.SecurityCode
                },
                InstallmentCount = request.Transaction.InstallmentCount
            };

            var createSaleRequest = new // Mock CreateSaleRequest
            {
                CreditCardTransactionCollection = new Collection<dynamic>(new dynamic[] { transaction }),
                Order = new { OrderReference = request.OrderId }
            };
            
            // Here the request would be made and the http response would be paarsed, mocking the response instead.

            //var merchantKey = Guid.Parse(merchantId);
            //var serviceClient = new GatewayServiceClient(merchantKey, new Uri("https://transaction.stone.com.br"));
            //var httpResponse = serviceClient.Sale.Create(createSaleRequest);

            return ApiResponseMock.MockStoneResponse(request);
        }

        /// <summary>
        /// Turns the domain objects into the Cielo API json strings
        /// to mocks the request and subsequent response.
        /// </summary>
        public static Entity.Operators.Response MakeCieloRequest(Entity.Operators.Request request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);           
            return ApiResponseMock.MockCieloResponse(jsonRequest);
        }

        // Makes a request to the ClearSale API.
        public static bool MakeAntiFraudRequest(Entity.AntiFraud.Request request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var responses = ApiResponseMock.MockClearSaleResponse(request.Orders, jsonRequest);
            return responses.All(r => r.AllValid) ? true : false;
        }


    } //class
} //namespace
