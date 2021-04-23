using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Infrastructure.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly AppDbContext dbContext;

        public CommentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comments = await dbContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<int> InsertComment(Comment comment)
        {
            int result = 0;
            dbContext.Comments.Add(comment);
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateComment(Comment comment)
        {
            int result = 0;
            dbContext.Entry(comment).State = EntityState.Modified;
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteComment(int id)
        {
            int result = 0;
            Comment comment = await GetComment(id);
            dbContext.Comments.Remove(comment);
            result = await dbContext.SaveChangesAsync();
            return result;
        }
    }
}
