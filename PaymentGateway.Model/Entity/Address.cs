

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

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string Country { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Equality implementation.
        /// </summary>
        /// <param name="other">Other address instance</param>
        public bool Equals(Address other)
        {
            return City == other.City
                && State == other.State
                && ZipCode == other.ZipCode
                && Country == other.Country
                && AddressLine1 == other.AddressLine1
                && AddressLine2 == other.AddressLine2;
        }


    } //class
} //namespace
