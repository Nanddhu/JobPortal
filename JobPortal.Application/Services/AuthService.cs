using JobPortal.Application.Interfaces;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BCrypt.Net;



namespace JobPortal.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<string> Register (User user)
        {
            if(string.IsNullOrEmpty(user.Password))
            {
                return "Password Required";
            }
            var exists = await _userRepository.ExistsAsync(user.Email);
            if(exists) 
                return "User Already Exists";

            user.PasswordHash= BCrypt.Net.BCrypt.HashPassword(user.Password);    
            await _userRepository.AddAsync(user);
            return " User Registered";


        }
        public async Task<string> Login  (User user)
        {
            var existingUser= await _userRepository.GetEmailAsync(user.Email);

            if (existingUser == null)
                return "Invalid Credentials";
            if (!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.PasswordHash))
                return "Invalid Credentials";

            return GenerateToken(existingUser);
        }
        private string GenerateToken (User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
             );
             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.UtcNow.AddHours(2),
                   signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
