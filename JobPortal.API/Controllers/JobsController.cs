using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobPortal.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IApplicationService _applicationservice;
        public JobsController(IJobService jobService, IApplicationService applicationservice)
        {
            _jobService = jobService;
            _applicationservice = applicationservice;
        }
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search ([FromQuery]string keyword = "")
        {
            var jobs=await _jobService.SearchJobs(keyword);
            return Ok(jobs);
        }
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyJob(
            [FromForm] ApplyJobRequest request,
            IFormFile resume)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var result = await _applicationservice.ApplyJob(
                userId,
                request.JobTitle,
                request.Company,
                resume);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("my-applications")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _applicationservice.GetUserApplications(userId);

            return Ok(result);
        }
    }
}
