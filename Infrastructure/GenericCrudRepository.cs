using Core;
using Core.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericCrudRepository<T> : IGenericCrudRepository<T>
        where T : StorableData
    {
        private readonly IMongoCollection<T> _collection;
        public GenericCrudRepository(IMongoDatabase database)
        {
            _collection =
                database.GetCollection<T>(CoreUtils.GetStorableDataName<T>());
        }
        public Task Create(T entity) =>
            _collection.InsertOneAsync(entity);

        public Task Delete(T entity) =>
            _collection.DeleteOneAsync(x => x.Id == entity.Id);

        public Task<List<T>> GetAll() =>
            _collection.Find(_ => true).ToListAsync();

        public async Task<T?> Get(int id)
        {

            var result = await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (result == default)
                return null;
            return result;
        }

        public async Task<int?> GetMaxId()
        {
            var query =
                "{ $group: { _id: \"MaxQueryResult\", Max: { $max: \"$_id\" } } } ";
            var queryResult = await _collection
                .Aggregate()
                .AppendStage<MaxQueryResult>(query)
                .FirstOrDefaultAsync();
            return queryResult?.Max;
        }

        public Task Update(T entity) =>
            _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }
}