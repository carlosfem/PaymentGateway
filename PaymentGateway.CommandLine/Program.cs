
using System;
using System.IO;

using PaymentGateway.Model.DAL;
using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;
using AntiFraud = PaymentGateway.Model.Entity.AntiFraud;


namespace PaymentGateway.CommandLine
{

    class Program
    {
        static void Main(string[] args)
        {

            //var people = PersonRepository.GetPerson();
            ////var person = PersonRepository.GetPerson("10");
            //var card = PersonRepository.GetCard("123456");
            //var operators = StoreRepository.GetStoreOperators(1);

            //var store = StoreRepository.GetStore(1);
            //var store2 = StoreRepository.GetStore(3);

            //var req = JsonConvert.DeserializeObject<Entity.Operators.Request>(jsonRequest);

            var gate = new Gateway(1);
            var card = PersonRepository.GetCard("123456");

            var items = new AntiFraud.Item[]
            {
                new AntiFraud.Item("1", "Item1", 10, 2),
                new AntiFraud.Item("2", "Item2", 20, 3),
                new AntiFraud.Item("3", "Item3", 30, 4),
                new AntiFraud.Item("4", "Item4", 40, 5)
            };

            gate.MakeRequest(items, 1, card, 1);
        }


    } //class
} //namespace
