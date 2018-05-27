
using System;
using System.IO;

using PaymentGateway.Model.DAL;
using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Repository;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.CommandLine
{

    class Program
    {
        static void Main(string[] args)
        {
            var db = new DbGateway();

            var people = PersonRepository.GetPerson();
            //var person = PersonRepository.GetPerson("10");
            var card = PersonRepository.GetCard("123456");
            var operators = StoreRepository.GetStoreOperators(1);

            var json_serializer = new JavaScriptSerializer();
        }


    } //class
} //namespace
