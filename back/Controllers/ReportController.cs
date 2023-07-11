using dev_ex.data;
using dev_ex.models;
using Microsoft.AspNetCore.Mvc;

namespace dev_ex.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public ReportController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public ActionResult<List<Report>> GetAllReports()
        {
            List<Report> reports = _dataAccess.GetAllReports();
            return reports;
        }

        [HttpPost]
        public ActionResult<Report> CreateReport(Report report, int managerId)
        {
            bool created = _dataAccess.CreateReport(report, managerId);
            if (created)
            {
                return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
            }
            return BadRequest("Report could not be created");
        }

        [HttpGet("{id}")]
        public ActionResult<Report> GetReportById(int id)
        {
            Report report = _dataAccess.GetReportById(id);
            if (report != null)
            {
                return report;
            }
            return NotFound();
        }

        [HttpGet("worker/{workerId}")]
        public ActionResult<List<Report>> GetAllReportsForWorker(int workerId)
        {
            List<Report> reports = _dataAccess.GetAllReportsForWorker(workerId);
            return reports;
        }

        [HttpGet("manager/{managerId}")]
        public ActionResult<List<Report>> GetAllReportsForManager(int managerId)
        {
            List<Report> reports = _dataAccess.GetAllReportsForManager(managerId);
            return reports;
        }
    }
}
