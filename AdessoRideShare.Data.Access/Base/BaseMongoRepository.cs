using AdessoRideShare.Data.Model.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AdessoRideShare.Data.Access.Base
{
    /// <summary>
    /// MongoDB repository
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseMongoRepository<TModel>
        where TModel : BaseEntity
    {
        private readonly IMongoCollection<TModel> mongoCollection;

        public BaseMongoRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<TModel>(collectionName);
        }

        public virtual List<TModel> GetList()
        {
            return mongoCollection.Find(book => true).ToList();
        }

        public virtual TModel GetById(string id)
        {
            return mongoCollection.Find<TModel>(m => m.Id == id).FirstOrDefault();
        }

        public virtual IEnumerable<TModel> FilterBy(Expression<Func<TModel, bool>> filterExpression)
        {
            return mongoCollection.Find(filterExpression).ToEnumerable();
        }

        public virtual TModel Create(TModel model)
        {
            mongoCollection.InsertOne(model);
            return model;
        }

        public virtual void Update(string id, TModel model)
        {
            mongoCollection.ReplaceOne(m => m.Id == id, model);
        }

        public virtual void Delete(TModel model)
        {
            mongoCollection.DeleteOne(m => m.Id == model.Id);
        }

        public virtual void Delete(string id)
        {
            mongoCollection.DeleteOne(m => m.Id == id);
        }
    }
}
