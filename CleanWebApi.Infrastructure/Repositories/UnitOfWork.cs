using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly IPostRepository postRepository;
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Comment> commentRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IPostRepository PostRepository => postRepository ?? new PostRepository(dbContext); // si es nulo, se crea uno nuevo, sino retorna el existente

        public IRepository<User> UserRepository => userRepository ?? new BaseRepository<User>(dbContext);

        public IRepository<Comment> CommentRepository => commentRepository ?? new BaseRepository<Comment>(dbContext);

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
