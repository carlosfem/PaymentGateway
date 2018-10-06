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
        private const int APPROVED_STATUS = 1;
        private const int UNAPPROVED_STATUS = 3;
        private const string APPROVED_CODE = "0";
        private const string UNAPPROVED_CODE = "4";
        private const string APPROVED_MSG = "Transaction Approved!";
        private const string UNAPPROVED_MSG = "Transaction Denied!";


        public Response(Request request, ResponseStatus status)
        {
            Request       = request;
            Status        = status == ResponseStatus.Approved ? APPROVED_STATUS : UNAPPROVED_STATUS;
            ReturnCode    = status == ResponseStatus.Approved ? APPROVED_CODE : UNAPPROVED_CODE;
            ReturnMessage = status == ResponseStatus.Approved ? APPROVED_MSG : UNAPPROVED_MSG;
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
