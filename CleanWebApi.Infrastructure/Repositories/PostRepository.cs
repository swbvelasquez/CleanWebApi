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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        //Cuando heredas de una clase con constructor, debes enviar los parametros que solicita, en este caso el contexto
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await entities.Where(x=>x.UserId== userId).ToListAsync();
        }
    }
}
