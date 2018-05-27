using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class OrderStatus
    {
        public OrderStatus(string id, string status, decimal score)
        {
            IdOrder = id;
            Status = status;
            Score = score;
        }

        public string IdOrder { get; private set; }
        public string Status { get; private set; }
        public decimal Score { get; private set; }


    } //class
} //namespace
