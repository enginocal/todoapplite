using System;

namespace TodoAppLite.Common
{
    public class TodoException : Exception
    {
        public string Code { get; set; }

        public TodoException()
        {

        }

        public TodoException(string code)
        {
            Code = code;
        }

        public TodoException(string message, params object[] args)
            : this(string.Empty, message, args)
        {

        }

        public TodoException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {

        }

        public TodoException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {

        }
    }
}
