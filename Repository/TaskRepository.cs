using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Tasks_Project.Models;
using Tasks_Project.Utils;

namespace Tasks_Project.Repository
{
    /// <summary>
    /// implements ITaskRepository interface
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        IHttpContextAccessor taskRepo;

        const string TasksSessionKey = "TasksStore";
        public TaskRepository(IHttpContextAccessor repo)
        {
            this.taskRepo = repo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task Create(TaskFile file)
        {
            var httpContext = taskRepo.HttpContext;

            Task task = new Task();
            string url = "";
            url = UploadFile.Upload(file.File);
            task = new Task
            {
                Description = file.Description,
                ImageUrl = url
            };
            List<Task> tasksInStore = httpContext.Session.GetListObject<Task>(TasksSessionKey);
            if (tasksInStore == null)
            {
                tasksInStore = new List<Task>();
            }
            task.Id = tasksInStore.Any() ?
               tasksInStore.Max(t => t.Id) + 1 : 1;


            tasksInStore.Add(task);

            httpContext.Session.SetSession(TasksSessionKey, tasksInStore);
            return task;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Task> GetAll()
        {
            var httpContext = taskRepo.HttpContext;
            var isExists = httpContext.Session.IsSessionExits(TasksSessionKey);
            if (!isExists)
            {
                var lst = new List<Task>();
                httpContext.Session.SetSession(TasksSessionKey, lst);
            }

            List<Task> tasksInStore = httpContext.Session.GetListObject<Task>(TasksSessionKey);
            return tasksInStore;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task GetById(int id)
        {
            var httpContext = taskRepo.HttpContext;
            var isExists = httpContext.Session.IsSessionExits(TasksSessionKey);
            if (!isExists)
            {
                var lst = new List<Task>();
                httpContext.Session.SetSession(TasksSessionKey, lst);
            }

            List<Task> tasksInStore = httpContext.Session.GetListObject<Task>(TasksSessionKey);
            Task task = tasksInStore.FirstOrDefault(p => p.Id == id);
            return task;
        }
    }
}
