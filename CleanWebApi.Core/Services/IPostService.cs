using CleanWebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Services
{   //Capa de logica de negocio, deberia ir en proyecto a parte si es mas complejo
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<int> InsertPost(Post post);
        Task<int> UpdatePost(Post post);
        Task<int> DeletePost(int id);
    }
}
