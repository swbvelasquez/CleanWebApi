using CleanWebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanWebApi.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetComments();
        Task<Comment> GetComment(int id);
        Task<int> InsertComment(Comment comment);
        Task<int> UpdateComment(Comment comment);
        Task<int> DeleteComment(int id);
    }
}
