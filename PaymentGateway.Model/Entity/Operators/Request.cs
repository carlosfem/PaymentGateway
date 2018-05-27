
using System.Collections.ObjectModel;


namespace PaymentGateway.Model.Entity.Operators
{
    public class Request
    {
        public Collection<Transaction> Transactions { get; set; }
        public Order Order { get; set; }

    } //classRequest


    public class Order
    {
        public string OrderReference { get; set; }

    } //classOrder


} //namespace
