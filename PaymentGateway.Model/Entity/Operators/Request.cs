
using Newtonsoft.Json;


namespace PaymentGateway.Model.Entity.Operators
{
    public class Customer
    {
        public Customer(string name) { Name = name; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Wraps requests to an operator's API.
    /// </summary>
    public class Request
    {
        public Request() { }

        public Request(Transaction transaction, string orderIdentifier)
        {
            Transaction = transaction;
            OrderId = orderIdentifier;
            Customer = new Customer(transaction.CreditCard.Holder.Name);
        }

        [JsonProperty("Payment")]
        public Transaction Transaction { get; set; }

        [JsonProperty("MerchantOrderId")]
        public string OrderId { get; set; }

        [JsonProperty("Customer")]
        public Customer Customer { get; set; }


    } //class
} //namespace
