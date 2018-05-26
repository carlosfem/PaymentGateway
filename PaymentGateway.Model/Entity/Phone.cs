

namespace PaymentGateway.Model.Entity
{
    public class Phone
    {
        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Phone(int type, int areaCode, int number)
        {
            Type     = type;
            AreaCode = areaCode;
            Number   = number;
        }

        /// <summary>
        /// Sets the optional fields
        /// </summary>
        public void SetOptionalFields(int countryCode)
        {
            CountryCode = countryCode;
        }

        public int Type { get; private set; }
        public int AreaCode { get; private set; }
        public int Number { get; private set; }

        public int CountryCode { get; set; }

    } //class
} //namespace
