using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Exceptions
{
    public class CityNotValidException : Exception
    {
        public CityNotValidException()
        {
        }

        public CityNotValidException(string message)
            : base(message)
        {
        }

        public CityNotValidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
