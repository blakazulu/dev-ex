using Microsoft.AspNetCore.Mvc;
using dev_ex.data;
using Task = dev_ex.models.Task;

namespace dev_ex.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public TasksController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public ActionResult<List<Task>> GetAllTasks()
        {
            List<Task> tasks = _dataAccess.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("worker/{id}")]
        public ActionResult<List<Task>> GetAllTasksForWorker(int id)
        {
            List<Task> tasks = _dataAccess.GetAllTasksForWorker(id);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            Task task = _dataAccess.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<Task> CreateTask(Task task)
        {
            var taskCreated = _dataAccess.CreateTask(task);
            return taskCreated ? Ok(task) : BadRequest("Task could not be created.");
        }
    }
}
