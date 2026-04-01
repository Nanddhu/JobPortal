using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.DTOs
{
    public class ApplyJobRequest
    {
       
        public string JobTitle { get; set; } = string.Empty;
        public string Company {  get; set; }=string.Empty;
    }
}
