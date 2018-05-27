using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using PaymentGateway.Model.DAL;
using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Entity.Operators;


namespace PaymentGateway.Model.Repository
{
    /// <summary>
    /// Repository for CRUD operations with the Person and related objects.
    /// </summary>
    public static class PersonRepository
    {
        /// <summary>
        /// Creates a dummy Phone instance.
        /// </summary>
        public static Phone GetDummyPhone()
        {
            var phone = new Phone(0, 21, 987654321);
            phone.SetOptionalFields(55);
            return phone;
        }

        /// <summary>
        /// Creates a dummy Address instance.
        /// </summary>
        public static Address GetDummyAddress()
        {
            var address = new Address("Rio de Janeiro", "RJ", "20271-090");
            address.SetOptionalFields("Brazil", "Av. Ataulfo de Paiva", "");
            return address;
        }


        /// <summary>
        /// Gets all of the Person registers on the Person table.
        /// </summary>
        public static IEnumerable<Person> GetPerson()
        {
            var sqlQuery = "select * from Person";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            return table.Rows.Cast<DataRow>().Select(x => (Person)x);
        }

        /// <summary>
        /// Gets a specific Person register based on the id.
        /// </summary>
        /// <param name="id">Id of the person being searched</param>
        public static Person GetPerson(string id)
        {
            var sqlQuery = $"select * from Person where Id = '{id}'";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            var person = (Person)table.Rows.Cast<DataRow>().FirstOrDefault();
            return person;
        }

        /// <summary>
        /// Gets a CreditCard register based on the number.
        /// </summary>
        /// <param name="id">Id of the person being searched</param>
        public static CreditCard GetCard(string number)
        {
            var sqlQuery = $"select * from CreditCard where Number = '{number}'";
            var db = new DbGateway();
            var table = db.Read(sqlQuery);
            var person = (CreditCard)table.Rows.Cast<DataRow>().FirstOrDefault();
            return person;
        }




    } //class
} //namespace
