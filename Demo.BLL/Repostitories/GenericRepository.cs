using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostitories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public IMongoCollection<T> _collection;
        public GenericRepository(MongoDbContext context , string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName);
        }
        public void Add(T entity)
        {
            if (entity is not null)
            {
                 _collection.InsertOne(entity);
            }
        }

        public void Delete(string id)
        {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq("_id", objectId);
                _collection.DeleteOne(filter);
        }

        public async Task<T> Get(string id)
        {
            var objectid = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectid);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
            
        }

        public void Update(T entity,string id)
        {
            var objectid = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectid);
            _collection.ReplaceOne(filter, entity);

        }
    }
}
