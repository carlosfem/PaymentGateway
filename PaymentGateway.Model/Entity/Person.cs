using System;
using System.Data;
using System.Collections.Generic;

using PaymentGateway.Model.Business;
using PaymentGateway.Model.Repository;


namespace PaymentGateway.Model.Entity
{
    public class Person
    {
        public Person()
        {

        }

        /// <summary>
        /// Class constructor, requires all mandatory fields.
        /// </summary>
        public Person(string id, int type, string name, Address address, IEnumerable<Phone> phones)
        {
            ID      = id;
            Type    = type;
            Name    = name;
            Address = address;
            Phones  = phones;
        }

        /// <summary>
        /// Person identifier.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Person type Person (1) or Company (2).
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        /// Person name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Person address.
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// Person phone.
        /// </summary>
        public IEnumerable<Phone> Phones { get; private set; }

        // Optional
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }


        /// <summary>
        /// Conversion from a DataRow into a Person instance.
        /// </summary>
        public static explicit operator Person(DataRow row)
        {
            if (row is null)
                throw new InvalidOperationException("Person not found!");

            var id        = Helpers.ConvertFromDBVal<string>(row["Id"]);
            var type      = Helpers.ConvertFromDBVal<int>(row["Type"]);
            var name      = Helpers.ConvertFromDBVal<string>(row["Name"]);
            var gender    = Helpers.ConvertFromDBVal<string>(row["Gender"]);
            var email     = Helpers.ConvertFromDBVal<string>(row["Email"]);
            var birthdate = Helpers.ConvertFromDBVal<DateTime?>(row["BirthDate"]);

            var address = PersonRepository.GetDummyAddress();
            var phone = PersonRepository.GetDummyPhone();
            var person = new Person(id, type, name, address, new Phone[] { phone });
            person.Gender = gender;
            person.Email = email;
            person.BirthDate = birthdate;

            return person;
        }


    } //class
} //namespace
