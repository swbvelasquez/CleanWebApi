using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanWebApi.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x=>x.PostId==id);
            return post;
        }

        public async Task<int> InsertPost(Post post)
        {
            int result = 0;
            dbContext.Posts.Add(post);
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdatePost(Post post)
        {
            int result = 0;
            dbContext.Entry(post).State = EntityState.Modified;
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeletePost(int id)
        {
            int result = 0;
            Post post = await GetPost(id);
            dbContext.Posts.Remove(post);
            result = await dbContext.SaveChangesAsync();
            return result;
        }
    }
}
