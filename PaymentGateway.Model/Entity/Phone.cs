﻿

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

        public int Type { get; set; }
        public int AreaCode { get; set; }
        public int Number { get; set; }

        public int CountryCode { get; set; }

        /// <summary>
        /// Equality implementation.
        /// </summary>
        /// <param name="other">Other phone instance</param>
        public bool Equals(Phone other)
        {
            return Type == other.Type
                && AreaCode == other.AreaCode
                && Number == other.Number
                && CountryCode == other.CountryCode;
        }


    } //class
} //namespace
