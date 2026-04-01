using JobPortal.Application.Interfaces;
using JobPortal.Application.Services;
using JobPortal.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Tests.Services
{
    public class AuthServiceTests
    {

        [Fact]
        public async Task Register_ShouldReturn_Success_WhenUserIsNew()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            var config = new ConfigurationBuilder().
                AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:SecretKey", "ThisIsMyVerySecureSecretKeyForJwtToken1234567890" }
                }).Build();
            var service = new AuthService(mockRepo.Object, config);
            var user = new User
            {
                Email = "test@gmail.com",
                Password = "12345678"
            };
            var result = await service.Register(user);

            Assert.Equal(" User Registered", result);
        }
        [Fact]
        public async Task Login_shouldReturnToken_WhenCredentialsAreValid()
        {
            var mockRepo = new Mock<IUserRepository>();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("12345678");
            mockRepo.Setup(repo => repo.GetEmailAsync(It.IsAny<string>())).ReturnsAsync(new User
            {
                Id = 1,
                Email = "test@gmail.com",
                PasswordHash = passwordHash
            });
            var config = new ConfigurationBuilder().
                AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "ThisIsMyVerySecureSecretKeyForJwtToken1234567890" }
                    }).Build();
            var service = new AuthService(mockRepo.Object, config);

            var user = new User
            {
                Email = "test@gmail.com",
                Password = "12345678"
            };
            var result = await service.Login(user);
            Assert.NotNull(result);
            Assert.NotEqual("Invalid Credentials", result);
        }
    }
}
