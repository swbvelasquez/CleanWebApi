using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<int> InsertPost(Post post);
        Task<int> UpdatePost(Post post);
        Task<int> DeletePost(int id);
    }
}
