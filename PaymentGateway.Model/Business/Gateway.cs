using System;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Entity.Operators;
using PaymentGateway.Model.Entity.AntiFraud;
using PaymentGateway.Model.Repository;


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
        /// Processes the anti-fraud step if necessary.
        /// </summary>
        /// <returns>
        /// True if the sale is authorized by the anti-fraud system or if the store doesnt use the system.
        /// </returns>
        private bool EvaluateAntiFraud(Transaction transaction)
        {
            if (Store.UseAntiFraud)
                return RequestManager.MakeAntiFraudRequest(transaction);
            else
                return true;
        }

        /// <summary>
        /// Makes a sale request.
        /// </summary>
        /// <param name="amount">Amount in cents</param>
        /// <param name="installments">Number of installments</param>
        /// <param name="card">Customer credit card</param>
        /// <param name="operatorIndex">Index of the operator</param>
        public void MakeRequest(decimal amount, int installments, CreditCard card, int operatorIndex)
        {
            if (operatorIndex > Store.Operators.Count() - 1)
                throw new InvalidOperationException("Invalid operator!");

            // Creates the transaction
            var transaction = new Transaction(amount, card, installments);

            // Check AntiFraud
            if (EvaluateAntiFraud(transaction))
            {
                // Makes to the operator
                var op = Store.Operators.ElementAt(operatorIndex);
                switch (op.Name)
                {
                    case "Cielo":
                        RequestManager.MakeCieloRequest(transaction);
                        break;
                    case "Stone":
                        RequestManager.MakeStoneRequest(transaction);
                        break;
                    default:
                        throw new NotImplementedException("Unknown operator!");
                }
            }
            else
            {
                transaction.Authorized = false;
                transaction.Message = "Transaction denied by the anti-fraud system.";
                StoreRepository.SaveTransaction(transaction);
            }
             
        }



    } //class
} //namespace
