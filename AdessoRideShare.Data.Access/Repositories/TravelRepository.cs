using AdessoRideShare.Data.Access.Base;
using AdessoRideShare.Data.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Data.Access.Repositories
{
    /// <summary>
    /// My repository that extends MongoDB repository
    /// </summary>
    public class TravelRepository : BaseMongoRepository<Travel>
    {
        public TravelRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }
    }
}
