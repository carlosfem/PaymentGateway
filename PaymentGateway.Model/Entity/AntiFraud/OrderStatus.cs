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

        public string IdOrder { get; set; }
        public string Status { get; set; }
        public decimal Score { get; set; }


    } //class
} //namespace
