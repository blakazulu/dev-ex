using dev_ex.data;
using dev_ex.models;
using Microsoft.AspNetCore.Mvc;

namespace dev_ex.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public WorkerController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public ActionResult<List<Worker>> GetAllWorkers()
        {
            List<Worker> workers = _dataAccess.GetAllWorkers();
            return workers;
        }

        [HttpGet("{id}")]
        public ActionResult<Worker> GetWorkerById(int id)
        {
            Worker worker = _dataAccess.GetWorkerById(id);
            return worker;
        }


        [HttpGet("employees")]
        public ActionResult<List<Worker>> GetAllEmployees()
        {
            List<Worker> employees = _dataAccess.GetAllEmployees();
            return employees;
        }

        [HttpGet("employee/{id}")]
        public ActionResult<Worker> GetEmployeeById(int id)
        {
            Worker employee = _dataAccess.GetEmployeeById(id);
            if (employee != null)
            {
                return employee;
            }
            return NotFound();
        }

        [HttpGet("managers")]
        public ActionResult<List<Worker>> GetAllManagers()
        {
            List<Worker> managers = _dataAccess.GetAllManagers();
            return managers;
        }

        [HttpGet("manager/{id}")]
        public ActionResult<Worker> GetManagerById(int id)
        {
            Worker manager = _dataAccess.GetManagerById(id);
            if (manager != null)
            {
                return manager;
            }
            return NotFound();
        }

        [HttpGet("manager/{id}/workers")]
        public ActionResult<List<Worker>> GetManagerWorkers(int id)
        {
            List<Worker> managerWorkers = _dataAccess.GetManagerWorkers(id);
            return managerWorkers;
        }
    }
}
