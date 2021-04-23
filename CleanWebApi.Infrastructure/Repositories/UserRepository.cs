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
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return user;
        }

        public async Task<int> InsertUser(User user)
        {
            int result = 0;
            dbContext.Users.Add(user);
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateUser(User user)
        {
            int result = 0;
            dbContext.Entry(user).State = EntityState.Modified;
            result = await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteUser(int id)
        {
            int result = 0;
            User user = await GetUser(id);
            dbContext.Users.Remove(user);
            result = await dbContext.SaveChangesAsync();
            return result;
        }
    }
}
