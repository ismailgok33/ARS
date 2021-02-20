using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Exceptions
{
    public class TravelNotFoundException : Exception
    {
        public TravelNotFoundException()
        {
        }

        public TravelNotFoundException(string message)
            : base(message)
        {
        }

        public TravelNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
