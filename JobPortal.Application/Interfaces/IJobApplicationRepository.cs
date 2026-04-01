using Hangfire.Common;
using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task AddAsync(JobApplication application);
        Task<bool> ExistsAsync(int userId, string title, string company);
        Task <JobApplication> GetByJobDetailsAsync(string jobTitle, string company);
        Task<List<JobApplication>> GetByUserIdAsync(int userId);
    }
}
