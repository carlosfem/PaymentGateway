
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


            var gate = new Gateway(1);
            var card = PersonRepository.GetCard("123456");
            gate.MakeRequest(1000, 1, card, 1);
        }


    } //class
} //namespace
