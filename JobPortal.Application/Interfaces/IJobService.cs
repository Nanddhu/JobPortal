using JobPortal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Interfaces
{
    public interface IJobService
    {
        Task<List<JobDto>> SearchJobs(string keyword);
    }
}
