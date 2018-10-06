using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Repository;
using Operators = PaymentGateway.Model.Entity.Operators;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.Model.Business
{
    /// <summary>
    /// Payment gateway object, core logic of the API.
    /// </summary>
    public class Gateway
    {
        public Gateway(int idStore)
        {
            Store = StoreRepository.GetStore(idStore);
        }


        /// <summary>
        /// Store instance.
        /// </summary>
        public Store Store { get; set; }


        /// <summary>
        /// Makes a sale request.
        /// </summary>
        /// <param name="items">Items to be sold</param>
        /// <param name="installments">Number of installments</param>
        /// <param name="card">Customer credit card</param>
        /// <param name="operatorIndex">Index of the operator</param>
        public void MakeRequest(IEnumerable<AntiFraud.Item> items, int installments, Operators.CreditCard card, int operatorIndex)
        {
            if (operatorIndex > Store.Operators.Count() - 1)
                throw new InvalidOperationException("Invalid operator!");

            // Creates the transaction
            var amount = items.Sum(i => i.ItemValue * i.Qty);
            var transaction = new Operators.Transaction(amount, card, installments);
            var orderId = Guid.NewGuid().ToString();

            // Check AntiFraud
            if (evaluateAntiFraud(items, transaction, orderId))
            {
                // Makes request to the operator
                var request = new Operators.Request(transaction, orderId);
                Operators.Response response;

                var op = Store.Operators.ElementAt(operatorIndex);
                switch (op.Brand)
                {
                    case OperatorBrandEnum.Cielo:
                        response = RequestManager.MakeCieloRequest(request);
                        break;
                    case OperatorBrandEnum.Stone:
                        response = RequestManager.MakeStoneRequest(request, Store.MerchantId);
                        break;
                    default:
                        throw new NotImplementedException("Unknown operator!");
                }

                handleOperatorResponse(response);
            }
            else
            {
                transaction.Authorized = false;
                transaction.Message = "Transaction denied by the anti-fraud system.";
                StoreRepository.SaveTransaction(transaction);
            }
             
        }


        /// <summary>
        /// Processes the anti-fraud step if necessary.
        /// </summary>
        /// <returns>
        /// True if the sale is authorized by the anti-fraud system or if the store doesnt use the system.
        /// </returns>
        private bool evaluateAntiFraud(IEnumerable<AntiFraud.Item> items, Operators.Transaction transaction, string orderId)
        {
            if (Store.UseAntiFraud)
            {
                var order = new AntiFraud.Order(Store, items, transaction, orderId);
                var orders = new List<AntiFraud.Order>() { order };
                var request = new AntiFraud.Request(Store.AntiFraudInfo.ApiKey, Store.AntiFraudInfo.LoginToken, orders, "BRA");
                return RequestManager.MakeAntiFraudRequest(request);
            }
            else
                return true;
        }


        /// <summary>
        /// Update and store the transaction based on the operator's response.
        /// </summary>
        private void handleOperatorResponse(Operators.Response response)
        {
            var transaction = response.Request.Transaction;
            transaction.Authorized = response.Status == 1;
            transaction.Message = response.ReturnMessage;
            StoreRepository.SaveTransaction(transaction);
        }



    } //class
} //namespace
