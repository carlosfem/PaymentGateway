using System;
using System.Data;

using PaymentGateway.Model.Business;


namespace PaymentGateway.Model.Entity
{
    public class AntiFraudInfo
    {
        public AntiFraudInfo(string key, string token)
        {
            ApiKey     = key;
            LoginToken = token;
        }


        public string ApiKey { get; private set; }
        public string LoginToken { get; private set; }


        /// <summary>
        /// Conversion from a DataRow into a Store instance.
        /// </summary>
        public static explicit operator AntiFraudInfo(DataRow row)
        {
            if (row is null)
                return null;

            var key   = Helpers.ConvertFromDBVal<string>(row["ApiKey"]);
            var login = Helpers.ConvertFromDBVal<string>(row["LoginToken"]);

            return new AntiFraudInfo(key, login);
        }


    } //class
} //namespace
