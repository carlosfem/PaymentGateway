using System;
using System.Data;
using System.Collections.Generic;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


namespace PaymentGateway.Model.Entity
{
    public class Store
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public Store(int id, string name, IEnumerable<Operator> operators, bool useAntiFraud)
        {
            ID           = id;
            Name         = name;
            Operators    = operators;
            UseAntiFraud = useAntiFraud;
        }

        /// <summary>
        /// Operator identifier.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Operator name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Credit card operators with which the store o
        /// </summary>
        public IEnumerable<Operator> Operators { get; private set; }

        /// <summary>
        /// Indicates if the store uses an anti-fraud system.
        /// </summary>
        public bool UseAntiFraud { get; private set; }

        /// <summary>
        /// Owner of the store.
        /// </summary>
        public Person Owner { get; private set; }


        /// <summary>
        /// Conversion from a DataRow into a Store instance.
        /// </summary>
        public static explicit operator Store(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Store not found!");

            var id           = Helpers.ConvertFromDBVal<int>(row["Id"]);
            var name         = Helpers.ConvertFromDBVal<string>(row["Name"]);
            var useAntiFraud = Helpers.ConvertFromDBVal<bool>(row["UseAntiFraud"]);
            var idOwner      = Helpers.ConvertFromDBVal<string>(row["IdOwner"]);
            var owner = PersonRepository.GetPerson(idOwner);

            try
            {
                var operators = StoreRepository.GetStoreOperators(id);
                return new Store(id, name, operators, useAntiFraud);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Invalid store. All stores must have at least one credit card operator.");
            }
        }


    } //class
} //namespace
