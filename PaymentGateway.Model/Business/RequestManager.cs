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
                AmountInCents = 10000,
                CreditCard = new
                {
                    CreditCardBrand  = card.CreditCardBrand,
                    CreditCardNumber = card.CreditCardNumber,
                    ExpMonth         = card.Expiration.Month,
                    ExpYear          = card.Expiration.Year,
                    HolderName       = card.Holder.Name,
                    SecurityCode     = card.SecurityCode
                },
                InstallmentCount = 1
            };

            var createSaleRequest = new // Mock CreateSaleRequest
            {
                CreditCardTransactionCollection = new Collection<dynamic>(new dynamic[] { transaction }),
                Order = new { OrderReference = request.OrderId }
            };
            
            // Cria o client que enviará a transação.
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
            var response = ApiResponseMock.MockClearSaleResponse(request.Orders, jsonRequest);

            // Here I'm assuming there's only one order to process, as was assumed throughout the API (easy to extend)
            return response.Orders.First().Status == "APA" ? true : false;
        }


    } //class
} //namespace
