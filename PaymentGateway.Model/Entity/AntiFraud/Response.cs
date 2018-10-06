
using System.Linq;
using System.Collections.Generic;


namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Response
    {
        private const string VALID_CODE = "APA";

        public Response(IEnumerable<OrderStatus> orders )
        {
            Orders = orders;
        }

        public IEnumerable<OrderStatus> Orders { get; set; }

        public bool AllValid => Orders.All(o => o.Status == VALID_CODE);


    } //class
} //namespace
