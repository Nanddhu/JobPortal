using Hangfire.Common;
using JobPortal.Application.Interfaces;
using JobPortal.Application.Services;
using JobPortal.Domain.Entities;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace JobPortal.Tests.Services
{
    public class JobServiceTests
    {
        [Fact]
        public async  Task SearchJobs_ShouldReturnList_WhenApiReturnsData()
        {
            var mockHttp = new Mock<HttpMessageHandler>();

            mockHttp.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{
                        ""results"": [
                            {
                                ""id"": ""123"",
                                ""title"": ""Software Developer"",
                                ""company"": { ""display_name"": ""Tech Company"" },
                                ""location"": { ""display_name"": ""New York"" },
                                ""salary_min"": 60000,
                                ""salary_max"": 80000,
                                ""contract_time"": ""Full-time"",
                                ""description"": ""Job description here""
                            }
                        ]
                    }")
                });
            var httpClient = new HttpClient(mockHttp.Object);
            var service = new JobService(httpClient);
            var result = await service.SearchJobs("developer");
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("123", result[0].Id);
            Assert.Equal("Software Developer", result[0].Title);

        }
        [Fact]
        public async Task ApplyJob_ShouldReturnError_WhenJobExpired()
        {

            var mockJobRepo = new Mock<IJobApplicationRepository>();
            var mockFile = new Mock<IFileService>();
            var mockUserRepo= new Mock<IUserRepository>();
            var mockEmail= new Mock<IEmailService>();
            var configMock=new Mock<IConfiguration>();

            mockJobRepo.Setup(x => x.GetByJobDetailsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new JobApplication
                {
                    ExpiryDate = DateTime.UtcNow.AddDays(-1)
                });
            var service = new ApplicationService(

                   mockJobRepo.Object,
                   configMock.Object,
                   mockUserRepo.Object,
                   mockEmail.Object,
                   mockFile.Object
                );

            var fakeFile = new Mock<IFormFile>();

            var result = await service.ApplyJob(1, "Dev", "ABC", fakeFile.Object);
            Assert.Equal("Job expired", result);
            

        }


    }
}
