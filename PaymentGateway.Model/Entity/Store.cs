using System;
using System.Collections.Generic;


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


    } //class
} //namespace
