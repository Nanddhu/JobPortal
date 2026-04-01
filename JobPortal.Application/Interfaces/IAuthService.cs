using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(User user);
        Task<string> Login(User user);
    }
}
