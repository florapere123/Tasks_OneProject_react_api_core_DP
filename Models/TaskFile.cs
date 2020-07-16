using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks_Project.Models
{
    public class TaskFile //: Task  
    { 
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
 
}
