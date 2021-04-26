using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Post>> GetPosts()
        {
            IEnumerable<Post> posts = await unitOfWork.PostRepository.GetAll();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            Post post = await unitOfWork.PostRepository.Get(id);
            return post;
        }

        public async Task<int> InsertPost(Post post)
        {
            User user = await unitOfWork.UserRepository.Get(post.UserId);

            if(user==null || user.Id == 0)
            {
                throw new Exception("User doesn't exist");
            }

            if (post.Description.ToLower().Contains("sexo"))
            {
                throw new Exception("Content not allowed");
            }

            int result = await unitOfWork.PostRepository.Insert(post);
            return result;
        }

        public async Task<int> UpdatePost(Post post)
        {
            int result = await unitOfWork.PostRepository.Update(post);
            return result;
        }

        public async Task<int> DeletePost(int id)
        {
            int result = await unitOfWork.PostRepository.Delete(id);
            return result;
        }
    }
}
