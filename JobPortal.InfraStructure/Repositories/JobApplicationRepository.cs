using Hangfire.Common;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using JobPortal.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly AppDbContext _context;

    public JobApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(JobApplication application)
    {
        await _context.JobApplications.AddAsync(application);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int userId, string title, string company)
    {
        return await _context.JobApplications
            .AnyAsync(x => x.UserId == userId &&
                           x.JobTitle == title &&
                           x.Company == company);
    }

    public async Task<List<JobApplication>> GetByUserIdAsync(int userId)
    {
        return await _context.JobApplications
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
    public async Task<JobApplication> GetByJobDetailsAsync(string jobTitle, string company)
    {
        return await _context.JobApplications
            .FirstOrDefaultAsync(j =>
                j.JobTitle == jobTitle &&
                j.Company == company);
    }
}