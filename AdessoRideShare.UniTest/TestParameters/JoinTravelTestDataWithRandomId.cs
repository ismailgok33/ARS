using AdessoRideShare.Api.Model.Dtos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdessoRideShare.UniTest.TestParameters
{
    class JoinTravelTestDataWithRandomId : TheoryData<JoinTravelParameters>
    {
        public JoinTravelTestDataWithRandomId()
        {
            Add(new JoinTravelParameters
            {
                TestJoinTravelDto = new JoinTravelDto { Id = "60312e57485f3bcd6fda8faa", PassengerCount = 3 }
            });
        }
    }
}
