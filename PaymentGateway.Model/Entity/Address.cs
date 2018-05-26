

namespace PaymentGateway.Model.Entity
{
    public class Address
    {
        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Address(string city, string state, string code)
        {
            // Mandatory
            City    = city;
            State   = state;
            ZipCode = code;

            // Optional
            Country      = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
        }

        /// <summary>
        /// Sets the optional fields
        /// </summary>
        public void SetOptionalFields(string country, string address1, string address2)
        {
            Country = country;
            AddressLine1 = address1;
            AddressLine2 = address2;
        }

        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }


    } //class
} //namespace
