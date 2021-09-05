using Core.Exceptions;
using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GenericCrudService<T> where T : StorableData
    {
        private protected readonly IGenericCrudRepository<T> _repo;

        public GenericCrudService(IGenericCrudRepository<T> repo)
        {
            _repo = repo;
        }

        public async virtual Task<T> Create(T entity)
        {
            entity.Id = (await _repo.GetMaxId() ?? 0) + 1;
            await _repo.Create(entity);
            return entity;
        }

        public async virtual Task<T> Delete(int id)
        {
            var entity = await Get(id);
            await _repo.Delete(entity);
            return entity;
        }

        public virtual Task<List<T>> GetAll() => _repo.GetAll();

        public async virtual Task<T> Get(int id)
        {
            var entity = await _repo.Get(id);
            if (entity == null)
                throw new DataNotFoundException<T>(id);
            return entity;
        }

        public async virtual Task<T> Update(T updatedEntity)
        {
            //Ensure that the entity exists
            await Get(updatedEntity.Id);
            await _repo.Update(updatedEntity);
            return updatedEntity;
        }
    }
}