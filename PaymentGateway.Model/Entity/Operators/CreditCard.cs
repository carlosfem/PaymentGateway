
using System;
using System.Data;
using Newtonsoft.Json;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


namespace PaymentGateway.Model.Entity.Operators
{
    [JsonObject("CreditCard")]
    public class CreditCard
    {
        /// <summary>
        /// Standard constructor, required for Json deserialization.
        /// </summary>
        public CreditCard()
        {

        }

        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public CreditCard(CreditCardBrandEnum brand, string number, DateTime expiration, Person holder, string security)
        {
            CreditCardBrand  = brand;
            CreditCardNumber = number;
            Expiration       = expiration;
            Holder           = holder;
            SecurityCode     = security;
        }



        [JsonIgnore()]
        public DateTime Expiration { get; set; }

        [JsonIgnore()]
        public CreditCardBrandEnum CreditCardBrand { get; set; }

        [JsonIgnore()]
        public Person Holder { get; set; }



        [JsonProperty("CardNumber")]
        public string CreditCardNumber { get; set; }

        [JsonProperty("Brand")]
        public string CreditCardBrandAsString => CreditCardBrand.GetDescription();

        [JsonProperty("Holder")]
        public string HolderName => Holder.Name;

        [JsonProperty("ExpirationDate")]
        public string ExpirationAsString
        {
            get
            {
                var strMonth = Expiration.Month.ToString();
                strMonth = strMonth.Length == 1 ? "0" + strMonth : strMonth;
                return $"{strMonth}/{Expiration.Year}";
            }
        }

        [JsonProperty("SecurityCode")]
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

            return new CreditCard(brand, number, expiration, holder, security);
        }


    } //class
} //namespace
