
using System.Collections.Generic;


namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Request
    {
        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Request(string key, string token, IEnumerable<Order> orders, string location)
        {
            ApiKey           = key;
            LoginToken       = token;
            Orders           = orders;
            AnalysisLocation = location;
        }

        /// <summary>
        /// User key to connect with the API.
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// Access token.
        /// </summary>
        public string LoginToken { get; private set; }

        /// <summary>
        /// Orders to send in the authentication request.
        /// </summary>
        public IEnumerable<Order> Orders { get; private set; }

        /// <summary>
        /// Location of analysis Brazil ("BRA") or United States ("USD").
        /// </summary>
        public string AnalysisLocation { get; private set; }


    } //class
} //namespace