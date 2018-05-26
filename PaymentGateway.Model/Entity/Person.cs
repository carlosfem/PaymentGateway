using System;
using System.Collections.Generic;


namespace PaymentGateway.Model.Entity
{
    public class Person
    {
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
        public string BirthDate { get; set; }


    } //class
} //namespace
