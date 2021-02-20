using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Exceptions
{
    public class NotEnoughSeatException : Exception
    {
        public NotEnoughSeatException()
        {
        }

        public NotEnoughSeatException(string message)
            : base(message)
        {
        }

        public NotEnoughSeatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
