
using System.Collections.Generic;


namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Response
    {
        public Response(IEnumerable<OrderStatus> orders )
        {
            Orders = orders;
        }

        public IEnumerable<OrderStatus> Orders { get; private set; }


    } //class
} //namespace
