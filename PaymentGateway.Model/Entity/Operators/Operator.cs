using System;


namespace PaymentGateway.Model.Entity
{
    public class Operator
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public Operator(int id, string name)
        {
            ID   = id;
            Name = name;
        }

        /// <summary>
        /// Operator identifier.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Operator name.
        /// </summary>
        public string Name { get; private set; }


    } //class
} //namespace
