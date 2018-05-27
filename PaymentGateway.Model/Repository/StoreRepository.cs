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
        public static Store GetStore(string id)
        {
            var sqlQuery = $"select * from Store where Id = '{id}'";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            var store = (Store)table.Rows.Cast<DataRow>().FirstOrDefault();
            return store;
        }

        /// <summary>
        /// Gets all operators of a given store.
        /// </summary>
        /// <param name="id">Store identifier</param>
        public static IEnumerable<Operator> GetStoreOperators(int id)
        {
            var sqlQuery = 
                $@"select * from Operator o
                    inner join AssociationStoreOperator a on a.IdOperator = o.Id
                    inner join Store s on s.Id = a.IdStore
                  where s.Id = {id}";

            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return table.Rows.Cast<DataRow>().Select(x => (Operator)x);
        }


    } //class
} //namespace
