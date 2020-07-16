using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks_Project.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        private DateTime createdDate = DateTime.Now.ToUniversalTime();
        public DateTime CreateDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

       
    }
 
}
