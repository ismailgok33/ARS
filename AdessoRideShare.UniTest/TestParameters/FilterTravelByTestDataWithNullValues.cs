using AdessoRideShare.Api.Model.Dtos;
using AdessoRideShare.Data.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdessoRideShare.UniTest.TestParameters
{
    public class FilterTravelByTestDataWithNullValues : TheoryData<FilterTravelByParameters>
    {
        public FilterTravelByTestDataWithNullValues()
        {
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = null, To = null, IsDirect = true }
            });
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = null, To = null, IsDirect = false }
            });
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = null, To = new City {X = 50, Y = 100 }, IsDirect = true }
            });
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = null, To = new City { X = 50, Y = 100 }, IsDirect = false }
            });
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = new City { X = 150, Y = 300 }, To = null, IsDirect = true }
            });
            Add(new FilterTravelByParameters
            {
                TestChangeStatusDto = new FilterTravelDto { From = new City { X = 150, Y = 300 }, To = null, IsDirect = false }
            });
        }
    }
}
