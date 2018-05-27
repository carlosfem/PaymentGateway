using System;
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


    } //class
} //namespace
