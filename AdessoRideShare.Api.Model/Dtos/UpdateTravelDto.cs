using AdessoRideShare.Data.Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Api.Model.Dtos
{
    public class UpdateTravelDto
    {
        public string Id { get; set; }
        public City From { get; set; }
        public City To { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AvailableSeats { get; set; }
        public bool IsPublished { get; set; }
    }
}
