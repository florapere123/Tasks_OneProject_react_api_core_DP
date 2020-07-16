using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks_Project.Models;
using Tasks_Project.Utils;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Newtonsoft.Json;
using Tasks_Project.Repository;

namespace Tasks_Project.Controllers
{
   
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    
    {
        //const string TasksSessionKey = "TasksStore";
        private readonly ILogger<TasksController> _logger;
        private ITaskRepository _taskRepo;

        public TasksController(ILogger<TasksController> logger , ITaskRepository repository)
        {
            _logger = logger;
            _taskRepo = repository;
        }



        [HttpGet]
        public ActionResult<List<Task>> GetAll() {
         var res = _taskRepo.GetAll();
         return res;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Task> GetById(int id)
        {
            var task = _taskRepo.GetById(id);
            #region snippet_ProblemDetailsStatusCode
            if (task == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Task not found");
            }
            #endregion

            return task;
        }


        /// <summary>
        /// creates new task with an image 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Task> Create([FromForm]TaskFile file)
        {
            
            try
            {
                var task = _taskRepo.Create(file);
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                {
                  _logger.LogError($"Something went wrong inside Create action: {ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
                }

            }
        }
 
    }
}
