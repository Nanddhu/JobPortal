using JobPortal.Application.DTOs;
using JobPortal.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace JobPortal.Application.Services
{
    public class JobService : IJobService
    {
        private readonly HttpClient _httpClient;
        
        public JobService(HttpClient httpClient )
        {
            _httpClient = httpClient;
           
        }
        public async Task<List<JobDto>> SearchJobs(string keyword)
        {

            var searchKeyword = string.IsNullOrWhiteSpace(keyword) ? "developer" : keyword;
            var encodedKeyword = Uri.EscapeDataString(keyword);

            var response = await _httpClient.GetAsync(
                $"https://api.adzuna.com/v1/api/jobs/in/search/1?app_id=4ec354cc&app_key=9d4b5ac1ce2329c0ff8d97054d0bf3f0&what={encodedKeyword}");

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var json = System.Text.Encoding.UTF8.GetString(bytes);
            var data = JObject.Parse(json);
            var jobs = new List<JobDto>();
            foreach (var item in data["results"])
            {
                jobs.Add(new JobDto
                {
                    Id = item["id"]?.ToString(), 
                    Title = item["title"]?.ToString(),
                    Company = item["company"]?["display_name"]?.ToString(),
                    Location = item["location"]?["display_name"]?.ToString(),

                    Salary = item["salary_min"] != null && item["salary_max"] != null
                    ? $"{item["salary_min"]} - {item["salary_max"]}"
                    : "Not disclosed",

                    Type = item["contract_time"]?.ToString() ?? "Full-time",

                    Description = item["description"]?.ToString(),

                    Tags = new List<string> { "Remote", "Urgent" }
                });
            }
            return jobs;
        }
       
    }
}
