using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Company {  get; set; }
        public string ResumeUrl { get; set; } 
        public DateTime AppliedAt { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
