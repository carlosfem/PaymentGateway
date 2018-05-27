using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;


namespace PaymentGateway.Model.Business
{
    public static class Helpers
    {
        /// <summary>
        /// Tries to convert an object into a new type, if the object is null, returns a default value.
        /// </summary>
        /// <typeparam name="T">Type after conversion</typeparam>
        /// <param name="obj">Object to convert</param>
        public static T ConvertFromDBVal<T>(object obj)
        {
            var result = (obj == null || obj == DBNull.Value)
                ? default(T)
                : (T)obj;

            return result;
        }

        /// <summary>
        /// Gets the description of an enum object.
        /// </summary>
        public static string GetDescription(this Enum myEnum)
        {
            var fieldInfo = myEnum.GetType().GetField(myEnum.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : myEnum.ToString();
        }


    } //class
} //namespace
