using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<T> entities;

        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> collection = await entities.ToListAsync();
            return collection;
        }

        public async Task<T> Get(int id)
        {
            T entity = await entities.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<int> Insert(T entity)
        {
            int result = 0;
            entities.Add(entity);
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(T entity)
        {
            int result = 0;
            dbContext.Entry(entity).State = EntityState.Modified;
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            T entity = await Get(id);
            entities.Remove(entity);
            result = await dbContext.SaveChangesAsync();
            return result;
        }
    }
}
