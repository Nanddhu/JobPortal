using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetEmailAsync(string email);
        Task<bool> ExistsAsync(string email);
        Task AddAsync(User user);
        Task<User> GetById(int userId);
    }
}
