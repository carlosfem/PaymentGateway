using System;
using Newtonsoft.Json;
using System.Collections.Generic;

using PaymentGateway.Model.Entity.Operators;
using PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Model.Business
{
    public static class RequestManager
    {
        /// <summary>
        /// Makes a request to the Cielo API and store the transaction including the response. 
        /// Stores the resulting transaction even if it is not authorized.
        /// </summary>
        /// <param name="transaction">Transaction to request</param>
        public static void MakeStoneRequest(Transaction transaction)
        {

        }

        /// <summary>
        /// Makes a request to the Cielo API and store the transaction including the response. . 
        /// Stores the resulting transaction even if it is not authorized.
        /// </summary>
        /// <param name="transaction">Transaction to request</param>
        public static void MakeCieloRequest(Transaction transaction)
        {
            var str = JsonConvert.SerializeObject(transaction);
            dynamic trans = JsonConvert.DeserializeObject<Transaction>(str);
        }

        // Makes a request to the ClearSale API.
        public static bool MakeAntiFraudRequest(Transaction transaction)
        {
            return true;
        }
        

    } //class
} //namespace
