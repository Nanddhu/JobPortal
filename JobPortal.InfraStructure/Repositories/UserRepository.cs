using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.InfraStructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }
        public async  Task<bool> ExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();  
        }
        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
    }
}
