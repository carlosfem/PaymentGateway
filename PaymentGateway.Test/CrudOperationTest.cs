using Xunit;
using System;
using System.Linq;

using PaymentGateway.Model.Entity;
using PaymentGateway.Model.Repository;
using PaymentGateway.Model.Entity.Operators;


namespace PaymentGateway.Test
{
    /// <summary>
    /// Unit test class for CRUD operations on the database.
    /// </summary>
    public class CrudOperationTest
    {
        [Fact]
        public void successfullyReadPerson()
        {
            var validPersonId = "1";
            var person = PersonRepository.GetPerson(validPersonId);

            Assert.Equal("Carlos", person.Name);
            Assert.Equal(validPersonId, person.ID);
            Assert.Equal(new DateTime(1991,5,26), person.BirthDate);
            Assert.Equal(1, person.Type);
            Assert.Equal("M", person.Gender);
            Assert.True(PersonRepository.GetDummyAddress().Equals(person.Address));
            Assert.True(PersonRepository.GetDummyPhone().Equals(person.Phones.First()));
        }

        [Fact]
        public void successfullyReadStore()
        {
            var validStoreId = 1;
            var store = StoreRepository.GetStore(validStoreId);

            Assert.Equal(1, store.ID);
            Assert.Equal("Loja do Carlos", store.Name);
            Assert.Equal("223.255.255.255", store.IpAddress);
            Assert.True(store.Operators.Count() == 3);
        }

        [Fact]
        public void successfullyReadCreditCard()
        {
            var validCardNumber = "123456";
            var card = PersonRepository.GetCard(validCardNumber);

            Assert.Equal(validCardNumber, card.CreditCardNumber);
            Assert.Equal(CreditCardBrandEnum.Elo, card.CreditCardBrand);
            Assert.Equal(new DateTime(2025,5,1), card.Expiration);
            Assert.Equal("123", card.SecurityCode);
            Assert.Equal("Carlos", card.HolderName);
        }

        [Fact]
        public void storeWithoutOperatorRaiseException()
        {
            var storeWithoutOperators = 3;
            Assert.Throws<InvalidOperationException>(() => {
                StoreRepository.GetStore(storeWithoutOperators);
            });
        }

        [Fact]
        public void storeWithStoneMustHaveMerchantId()
        {
            var storeWithStoneNoMerchantId = 2;
            Assert.Throws<InvalidOperationException>(() => {
                StoreRepository.GetStore(storeWithStoneNoMerchantId);
            });
        }

        [Fact]
        public void successfullyInsertAndDeleteTransaction()
        {
            var testMessage = "Test Transaction, delete after assertion";
            var card = PersonRepository.GetCard("123456");
            var transaction = new Transaction(1000, card, 1)
            {
                Authorized = true,
                Message = testMessage
            };

            // Inserts the transaction and assert
            StoreRepository.SaveTransaction(transaction);
            var fromDb = StoreRepository.GetTransactionByMessage(testMessage);
            Assert.NotNull(fromDb);
            StoreRepository.DeleteTransactionByMessage(testMessage);

            Assert.Throws<InvalidOperationException>(() => {
                StoreRepository.GetTransactionByMessage(testMessage);
            });
        }

        // TODO
        //[Fact]
        //public void insertionFailsForDuplicateStoreId()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public void insertionFailsForDuplicateStoreOperatorAssociation()
        //{

        //}

        //[Fact]
        //public void insertionFailsForCreditCardWithInvalidPerson()
        //{

        //}


    } //class
} //namespace
