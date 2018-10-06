using System;
using System.Data;
using System.ComponentModel;

using PaymentGateway.Model.Business;


namespace PaymentGateway.Model.Entity
{
    public enum OperatorBrandEnum
    {
        [Description("Cielo")]
        Cielo = 1,
        [Description("Stone")]
        Stone = 2,
        [Description("Desconhecido")]
        Desconhecido = 3
    }

    public class Operator
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public Operator(int id, string name)
        {
            ID    = id;
            Name  = name;
            Brand = (OperatorBrandEnum)Enum.Parse(typeof(OperatorBrandEnum), name);
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
        /// Brand of the operator.
        /// </summary>
        public OperatorBrandEnum Brand { get; private set; }


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
