using CleanWebApi.Core.CustomEntities;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Exceptions;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            //Cuando es un IEnumerable, aun no esta cargada la informacion en memoria, por tanto, se puede usar los filtros en esta clase, si usa un tolist se debe pasar los filtros hasta la parte del repositorio
            IEnumerable<Post> posts = unitOfWork.PostRepository.GetAll();

            if (filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            //paginacion
            PagedList<Post> pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber,filters.PageSize);

            return pagedPosts;
        }

        public async Task<Post> GetPost(int id)
        {
            return await unitOfWork.PostRepository.Get(id);
        }

        public async Task<int> InsertPost(Post post)
        {
            User user = await unitOfWork.UserRepository.Get(post.UserId);

            if(user==null || user.Id == 0)
            {
                throw new BusinessException("User doesn't exist");
            }

            if (post.Description.ToLower().Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }

            var userPosts = await unitOfWork.PostRepository.GetPostsByUser(user.Id);
            
            if (userPosts!=null && userPosts.Count() < 10)
            {
                var lastPost = userPosts.OrderByDescending(x=>x.Date).FirstOrDefault();

                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not to able to publish the post");
                }
            }

            await unitOfWork.PostRepository.Insert(post);
            int result = await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdatePost(Post post)
        {
            unitOfWork.PostRepository.Update(post);
            int result = await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeletePost(int id)
        {
            await unitOfWork.PostRepository.Delete(id);
            int result = await unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
