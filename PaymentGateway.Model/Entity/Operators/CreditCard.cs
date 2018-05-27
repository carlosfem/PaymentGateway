
using System;
using System.Data;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


namespace PaymentGateway.Model.Entity.Operators
{
    public class CreditCard
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public CreditCard(CreditCardBrandEnum brand, string number, int month, int year, Person holder, string security)
        {
            CreditCardBrand  = brand;
            CreditCardNumber = number;
            ExpMonth         = month;
            ExpYear          = year;
            Holder           = holder;
            SecurityCode     = security;
        }

        public CreditCardBrandEnum CreditCardBrand { get; set; }
        public string CreditCardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public Person Holder { get; set; }
        public string SecurityCode { get; set; }


        /// <summary>
        /// Conversion from a DataRow into a CreditCard instance.
        /// </summary>
        public static explicit operator CreditCard(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Credit card not found!");

            var number     = Helpers.ConvertFromDBVal<string>(row["Number"]);
            var brand      = Helpers.ConvertFromDBVal<CreditCardBrandEnum>(row["Brand"]);
            var expiration = Helpers.ConvertFromDBVal<DateTime>(row["Expiration"]);
            var security   = Helpers.ConvertFromDBVal<string>(row["SecurityCode"]);
            var idHolder   = Helpers.ConvertFromDBVal<string>(row["IdHolder"]);

            var holder = PersonRepository.GetPerson(idHolder);

            return new CreditCard(brand, number, expiration.Month, expiration.Year, holder, security);
        }


    } //class
} //namespace
