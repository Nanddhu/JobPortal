using JobPortal.Application.DTOs;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<string> ApplyJob(int userId, string jobTitle, string company, IFormFile resume);
        Task<List<JobApplication>> GetUserApplications(int userId);

      
    }

}
