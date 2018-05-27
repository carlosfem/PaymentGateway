﻿

namespace PaymentGateway.Model.Entity.AntiFraud
{
    public class Item
    {
        /// <summary>
        /// Class constructor, requires all of the mandatory fields.
        /// </summary>
        public Item(string id, string name, decimal value, int qty)
        {
            // Mandatory
            ID          = id;
            ProductName = name;
            ItemValue   = value;
            Qty         = qty;

            // Optional
            Gift         = 0;
            CategoryId   = 0;
            Categoryname = string.Empty;
        }

        /// <summary>
        /// Sets the optional fields
        /// </summary>
        public void SetOptionalFields(bool isGift, int categoryId, string categoryName)
        {
            Gift         = isGift ? 1 : 0;
            CategoryId   = categoryId;
            Categoryname = categoryName;
        }

        public string ID { get; private set; }
        public string ProductName { get; private set; }
        public decimal ItemValue { get; private set; }
        public int Qty { get; private set; }

        public int Gift { get; set; }
        public int CategoryId { get; set; }
        public string Categoryname { get; set; }


    } //class
} //namespace