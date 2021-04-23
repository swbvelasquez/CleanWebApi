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
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            IEnumerable<Post> posts = await postRepository.GetPosts();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            Post post = await postRepository.GetPost(id);
            return post;
        }

        public async Task<int> InsertPost(Post post)
        {
            User user = await userRepository.GetUser(post.UserId);

            if(user==null || user.UserId == 0)
            {
                throw new Exception("User doesn't exist");
            }

            if (post.Description.ToLower().Contains("sexo"))
            {
                throw new Exception("Content not allowed");
            }

            int result = await postRepository.InsertPost(post);
            return result;
        }

        public async Task<int> UpdatePost(Post post)
        {
            int result = await postRepository.UpdatePost(post);
            return result;
        }

        public async Task<int> DeletePost(int id)
        {
            int result = await postRepository.DeletePost(id);
            return result;
        }
    }
}
