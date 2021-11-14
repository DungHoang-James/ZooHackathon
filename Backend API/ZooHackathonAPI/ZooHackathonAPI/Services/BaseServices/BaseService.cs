using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZooHackathonAPI.Repository.BaseRepo;
using ZooHackathonAPI.UnitOfWorks;

namespace ZooHackathonAPI.Services.BaseServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Get<TKey>(TKey id);
        Task<TEntity> GetAsync<TKey>(TKey id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync();
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void Save();
        Task SaveAsync();
    }

    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected IBaseRepository<TEntity> repository;
        protected IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }
        public void Save()
        {
            unitOfWork.Commit();
        }

        public async Task SaveAsync()
        {
            await unitOfWork.CommitAsync();
        }

        public void Create(TEntity entity)
        {
            repository.Create(entity);
            Save();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await repository.CreateAsync(entity);
            repository.Save();
        }

        public void Delete(TEntity entity)
        {
            repository.Delete(entity);
            Save();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            repository.Delete(entity);
            await SaveAsync();
        }

        public TEntity FirstOrDefault()
        {
            return repository.FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await repository.FirstOrDefaultAsync();
        }

        public TEntity Get<TKey>(TKey id)
        {
            return repository.Get(id);
        }

        public IQueryable<TEntity> Get()
        {
            return repository.Get();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Get(predicate);
        }
        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return await repository.GetAsync(id);
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
            Save();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            repository.Update(entity);
            await repository.SaveAsync();
        }
    }
}
