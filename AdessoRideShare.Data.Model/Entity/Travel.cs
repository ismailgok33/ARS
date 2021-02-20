using AdessoRideShare.Data.Common.CommonModel;
using AdessoRideShare.Data.Model.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Data.Model.Entity
{
    public class Travel : BaseEntity
    {
        [BsonElement("From")]
        public City From { get; set; }

        [BsonElement("To")]
        public City To { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("AvailableSeats")]
        public int AvailableSeats { get; set; }
        [BsonElement("IsPublished")]
        public bool IsPublished { get; set; }
    }
}
