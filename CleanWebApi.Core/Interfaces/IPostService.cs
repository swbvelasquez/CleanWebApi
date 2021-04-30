using CleanWebApi.Core.CustomEntities;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Interfaces
{   //Capa de logica de negocio, deberia ir en proyecto a parte si es mas complejo
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);
        Task<int> InsertPost(Post post);
        Task<int> UpdatePost(Post post);
        Task<int> DeletePost(int id);
    }
}
