using System;
using System.Globalization;

namespace API.Exceptions
{
    public class P4pException : Exception
    {
        public P4pException() : base() { }

        public P4pException(string message) : base(message) { }

        public P4pException(string message, params object[] args) : 
            base(string.Format(CultureInfo.CurrentCulture, message, args)) 
        {
        }
    }
}
