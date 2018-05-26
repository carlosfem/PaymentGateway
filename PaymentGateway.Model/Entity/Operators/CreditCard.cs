

namespace PaymentGateway.Model.Entity.Operators
{
    public class CreditCard
    {
        /// <summary>
        /// Class constructor requires all mandatory fields.
        /// </summary>
        public CreditCard(CreditCardBrandEnum brand, string number, int month, int year, string holder, string security)
        {
            CreditCardBrand  = brand;
            CreditCardNumber = number;
            ExpMonth         = month;
            ExpYear          = year;
            HolderName       = holder;
            SecurityCode     = security;
        }

        public CreditCardBrandEnum CreditCardBrand { get; private set; }
        public string CreditCardNumber { get; private set; }
        public int ExpMonth { get; private set; }
        public int ExpYear { get; private set; }
        public string HolderName { get; private set; }
        public string SecurityCode { get; private set; }
        

    } //class
} //namespace
