using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Exceptions;
using CleanWebApi.Core.Interfaces;
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

        public IEnumerable<Post> GetPosts()
        {
            return unitOfWork.PostRepository.GetAll();
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
