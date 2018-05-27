using System;
using System.Data;

using PaymentGateway.Model.Business;


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


        /// <summary>
        /// Conversion from a DataRow into a Operator instance.
        /// </summary>
        public static explicit operator Operator(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Operator not found!");

            var id   = Helpers.ConvertFromDBVal<int>(row["Id"]);
            var name = Helpers.ConvertFromDBVal<string>(row["Name"]);
            return new Operator(id, name);
        }

        /// <summary>
        /// Equality implementation.
        /// </summary>
        /// <param name="other">Other operator instance</param>
        public bool Equals(Operator other)
        {
            return ID == other.ID && Name == other.Name;
        }


    } //class
} //namespace
