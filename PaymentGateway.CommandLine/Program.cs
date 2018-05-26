
using System;

using PaymentGateway.Model.Entity;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.CommandLine
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var req = new AntiFraud.Request();

            var item = new AntiFraud.Item()
            {
                ID = "oi"
            };
        }


    } //class
} //namespace
