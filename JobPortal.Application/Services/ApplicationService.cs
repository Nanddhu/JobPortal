using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Hangfire;

public class ApplicationService : IApplicationService
{
    private readonly IJobApplicationRepository _repo;
    private readonly IFileService _fileService;
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ApplicationService(IJobApplicationRepository repo,IConfiguration config, IUserRepository userRepository, IEmailService emailService, IFileService fileService)
    {
        _repo = repo;
        _config = config;
        _userRepository = userRepository;
        _emailService = emailService;
        _fileService = fileService;
    }

    public async Task<string> ApplyJob(int userId, string jobTitle, string company , IFormFile resume)
    {
        if (string.IsNullOrEmpty(jobTitle))
            return "Invalid job data";

        if (resume == null)
            return "Resume required";

        var Job=await _repo.GetByJobDetailsAsync(jobTitle, company);

        if(Job.ExpiryDate < DateTime.UtcNow)
            return "Job expired";

        var exists = await _repo.ExistsAsync(userId, jobTitle, company);

        if (exists)
            return "Already applied";

        var resumeUrl = await _fileService.UploadFileAsync(resume, $"jobportal/resumes/user_{userId}");

        var application = new JobApplication
        {
            UserId = userId,
            JobTitle = jobTitle,
            Company = company,
            ResumeUrl = resumeUrl,
            AppliedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(application);

        var user = await _userRepository.GetById(userId); 

        BackgroundJob.Enqueue(() =>
            _emailService.SendEmailAsync(
                user.Email,
                "Application Submitted",
                $"You have successfully applied for {jobTitle} at {company}."
            )
        );

        return "Application submitted";
    }

    public async Task<List<JobApplication>> GetUserApplications(int userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
    
}