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
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<User> userRepository;

        public PostService(IRepository<Post> postRepository, IRepository<User>  userRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            IEnumerable<Post> posts = await postRepository.GetAll();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            Post post = await postRepository.Get(id);
            return post;
        }

        public async Task<int> InsertPost(Post post)
        {
            User user = await userRepository.Get(post.UserId);

            if(user==null || user.Id == 0)
            {
                throw new Exception("User doesn't exist");
            }

            if (post.Description.ToLower().Contains("sexo"))
            {
                throw new Exception("Content not allowed");
            }

            int result = await postRepository.Insert(post);
            return result;
        }

        public async Task<int> UpdatePost(Post post)
        {
            int result = await postRepository.Update(post);
            return result;
        }

        public async Task<int> DeletePost(int id)
        {
            int result = await postRepository.Delete(id);
            return result;
        }
    }
}
