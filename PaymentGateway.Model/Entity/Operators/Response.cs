using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Model.Entity.Operators
{
    public enum ResponseStatus
    {
        Approved = 1,
        Denied = 2
    }

    public class Response
    {
        public Response(Request request, ResponseStatus status)
        {
            Request       = request;
            Status        = status == ResponseStatus.Approved ? 1 : 3;
            ReturnCode    = status == ResponseStatus.Approved ? "0" : "4";
            ReturnMessage = status == ResponseStatus.Approved 
                ? "Transaction Approved!" 
                : "Transaction Denied!";
        }

        /// <summary>
        /// Request information.
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Response status, Authorized (1) or denied (3).
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Return code from the operator, Authorized (0) or Denied (4).
        /// </summary>
        public string ReturnCode { get; set; }

        /// <summary>
        /// Message returned from the operator.
        /// </summary>
        public string ReturnMessage { get; set; }


    } //class
} //namespace
