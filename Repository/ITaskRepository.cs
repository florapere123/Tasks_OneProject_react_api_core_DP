using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks_Project.Models;

namespace Tasks_Project.Repository
{
    /// <summary>
    /// interface with 3 methods for tasks manager ui
    /// </summary>
    public interface ITaskRepository 
    {
        List<Task> GetAll();
        Task GetById(int id);
        Task Create(TaskFile file);
       // bool RemoveTask(int id); not implemented for now
    }
}
