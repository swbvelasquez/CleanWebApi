using CleanWebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> PostRepository { get;}
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
