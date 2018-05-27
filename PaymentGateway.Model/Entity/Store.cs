using System;
using System.Linq;
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
        public Store(int id, string name, IEnumerable<Operator> operators, AntiFraudInfo antiFraud, string merchantId, string ip)
        {
            ID            = id;
            Name          = name;
            Operators     = operators;
            AntiFraudInfo = antiFraud;
            MerchantId    = merchantId;
            IpAddress     = ip;
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
        /// Information about the anti fraud contract of the store.
        /// </summary>
        public AntiFraudInfo AntiFraudInfo { get; private set; }

        /// <summary>
        /// Indicates if the store uses an anti-fraud system.
        /// </summary>
        public bool UseAntiFraud => AntiFraudInfo is null ? false : true;

        /// <summary>
        /// Owner of the store.
        /// </summary>
        public Person Owner { get; private set; }

        /// <summary>
        /// Identifier for stores that work with Stone.
        /// </summary>
        public string MerchantId { get; private set; }

        /// <summary>
        /// Ip address of the store.
        /// </summary>
        public string IpAddress { get; private set; }


        /// <summary>
        /// Conversion from a DataRow into a Store instance.
        /// </summary>
        public static explicit operator Store(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Store not found!");

            var id           = Helpers.ConvertFromDBVal<int>(row["Id"]);
            var name         = Helpers.ConvertFromDBVal<string>(row["Name"]);
            var idOwner      = Helpers.ConvertFromDBVal<string>(row["IdOwner"]);
            var merchantId   = Helpers.ConvertFromDBVal<string>(row["MerchantId"]);
            var ipAddress    = Helpers.ConvertFromDBVal<string>(row["IpAddress"]);

            var owner     = PersonRepository.GetPerson(idOwner);
            var operators = StoreRepository.GetStoreOperators(id);
            var antiFraud = StoreRepository.GetStoreAntiFraudInfo(id);

            if (operators.Any(o => o.Name == "Stone") && merchantId is null)
                throw new InvalidOperationException("Invalid store. All stores working with Stone must have a merchant ID.");

            return new Store(id, name, operators, antiFraud, merchantId, ipAddress);
        }


    } //class
} //namespace
