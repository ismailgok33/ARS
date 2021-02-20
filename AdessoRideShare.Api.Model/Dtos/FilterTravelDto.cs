using AdessoRideShare.Data.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Api.Model.Dtos
{
    public class FilterTravelDto
    {
        public City From { get; set; }
        public City To { get; set; }
        public bool IsDirect { get; set; }
    }
}
