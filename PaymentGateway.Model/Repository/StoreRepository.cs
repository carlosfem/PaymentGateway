using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.DAL;
using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Entity.Operators;


namespace PaymentGateway.Model.Repository
{
    /// <summary>
    /// Repository for CRUD operations with the Store and related objects.
    /// </summary>
    public static class StoreRepository
    {
        /// <summary>
        /// Gets a specific Store register based on the id.
        /// </summary>
        /// <param name="id">Id of the store being searched</param>
        public static Store GetStore(int id)
        {
            var sqlQuery = $"select * from Store where Id = {id}";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            var store = (Store)table.Rows.Cast<DataRow>().FirstOrDefault();
            return store;
        }

        /// <summary>
        /// Gets all of the Operator registers on the Operator table.
        /// </summary>
        public static IEnumerable<Operator> GetOperator()
        {
            var sqlQuery = "select * from Operator";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return table.Rows.Cast<DataRow>().Select(x => (Operator)x);
        }

        /// <summary>
        /// Gets all operators associated to a given store.
        /// </summary>
        /// <param name="id">Store identifier</param>
        public static IEnumerable<Operator> GetStoreOperators(int id)
        {
            var sqlQuery = 
                $@"select o.* from Operator o
                    inner join AssociationStoreOperator a on a.IdOperator = o.Id
                    inner join Store s on s.Id = a.IdStore
                    where s.Id = {id}";

            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return table.Rows.Cast<DataRow>().Select(x => (Operator)x);
        }

        /// <summary>
        /// Gets all anti-fraud records associated to a given store.
        /// </summary>
        /// <param name="id">Store identifier</param>
        public static AntiFraudInfo GetStoreAntiFraudInfo(int id)
        {
            var sqlQuery = $@"select * from AssociationStoreAntiFraud where IdStore = {id}";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return (AntiFraudInfo)table.Rows.Cast<DataRow>().FirstOrDefault();
        }

        /// <summary>
        /// Gets the first transaction with a certain message.
        /// </summary>
        /// <param name="msg">Message to find</param>
        public static Transaction GetTransactionByMessage(string msg)
        {
            var sqlQuery = $@"select * from [Transaction] where Message = '{msg}'";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return (Transaction)table.Rows.Cast<DataRow>().FirstOrDefault();
        }


        /// <summary>
        /// Saves a transaction.
        /// </summary>
        /// <param name="transaction">Transaction to store</param>
        public static void SaveTransaction(Transaction transaction)
        {
            var pairs = new List<ExecValuePair>
            {
                new ExecValuePair("@cardNumber", transaction.CreditCard.CreditCardNumber),
                new ExecValuePair("@amount", transaction.AmountInCents),
                new ExecValuePair("@installments", transaction.InstallmentCount),
                new ExecValuePair("@authorized", transaction.Authorized),
                new ExecValuePair("@msg", transaction.Message)
            };
            var sqlQuery =
                $@"insert into [Transaction] (CardNumber, AmountInCents, Installments, Authorized, Message) 
                   values (@cardNumber, @amount, @installments, @authorized, @msg)";

            var db = new DbGateway();
            db.Exec(sqlQuery, pairs);
        }

        /// <summary>
        /// Deletes transactions based on their message.
        /// </summary>
        public static void DeleteTransactionByMessage(string msg)
        {
            var pairs = new List<ExecValuePair>
            {
                new ExecValuePair("@msg", msg)
            };
            var sqlQuery = $@"delete from [Transaction] where Message = @msg";
            var db = new DbGateway();
            db.Exec(sqlQuery, pairs);
        }


    } //class
} //namespace
