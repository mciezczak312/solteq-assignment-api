using System;
using System.Runtime.Serialization;

namespace EmployeesManagement.Infrastructure.Helpers
{
    [Serializable]
    internal class IllegalColumnNameException : Exception
    {
        public IllegalColumnNameException()
        {
        }

        public IllegalColumnNameException(string message) : base(message)
        {
        }

        public IllegalColumnNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IllegalColumnNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}