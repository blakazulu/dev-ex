using dev_ex.models;
using Newtonsoft.Json;
using Task = dev_ex.models.Task;

namespace dev_ex.data
{
    public class DataAccess
    {
        private string FilePath { get; }
        private string jsonData;
        private List<Worker> workers;
        private List<Task> tasks;
        private List<Report> reports;

        public DataAccess(string filePath)
        {
            FilePath = filePath;
            jsonData = ReadJsonData();
            initializeVariables();
        }

        private void initializeVariables()
        {
            var deserializedData = JsonConvert.DeserializeObject<JsonData>(jsonData);
            workers = deserializedData.Workers;
            tasks = deserializedData.Tasks;
            reports = deserializedData.Reports;
        }

        public string ReadJsonData()
        {
            return File.ReadAllText(FilePath);
        }

        public void WriteJsonTaskData(List<Task> newTasks)
        {
            // Merge the new tasks with the existing tasks
            tasks.AddRange(newTasks);

            var jsonData = new JsonData
            {
                Workers = workers,
                Tasks = tasks,
                Reports = reports
            };

            var jsonDataString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

            File.WriteAllText(FilePath, jsonDataString);
            initializeVariables();
        }

        public void WriteReportJsonData(Report report)
        {
            // Find the index of the report to update
            int reportIndex = reports.FindIndex(r => r.Id == report.Id);
            if (reportIndex != -1)
            {
                // Update the existing report with the new data
                reports[reportIndex] = report;
            }
            else
            {
                // Add the new report to the existing reports
                reports.Add(report);
            }

            var jsonData = new JsonData
            {
                Workers = workers,
                Tasks = tasks,
                Reports = reports
            };

            var jsonDataString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

            File.WriteAllText(FilePath, jsonDataString);
            initializeVariables();
        }

        public void WriteManagersJsonData(List<Worker> managers)
        {
            // Get all employees, tasks, and reports
            List<Worker> employees = GetAllEmployees();

            // Merge the managers list into the employees list
            List<Worker> workers = employees.Concat(managers).ToList();

            var jsonData = new JsonData
            {
                Workers = workers,
                Tasks = tasks,
                Reports = reports
            };

            var jsonDataString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

            File.WriteAllText(FilePath, jsonDataString);
        }

        public List<Worker> GetAllWorkers()
        {
            return workers;
        }

        public Worker GetWorkerById(int id)
        {
            Worker worker = workers.FirstOrDefault(e => e.Id == id);
            return worker;
        }

        // CRUD operations for Employees

        public List<Worker> GetAllEmployees()
        {
            List<Worker> employees = workers.Where(w => w.EmployeesIds.Count == 0).ToList();
            return employees;
        }

        public Worker GetEmployeeById(int id)
        {
            List<Worker> employees = GetAllEmployees();
            Worker employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        // CRUD operations for Managers
        public List<Worker> GetAllManagers()
        {
            List<Worker> managers = workers.Where(w => w.EmployeesIds.Count > 0).ToList();
            return managers;
        }

        public Worker GetManagerById(int id)
        {
            List<Worker> managers = GetAllManagers();
            Worker manager = managers.FirstOrDefault(e => e.Id == id);
            return manager;
        }

        public List<Worker> GetManagerWorkers(int id)
        {
            List<int> employeesIds = GetManagerById(id).EmployeesIds;

            List<Worker> managerWorkers = workers.Where(w => employeesIds.Contains(w.Id)).ToList();
            return managerWorkers;
        }

        // CRUD operations for Tasks
        public List<Task> GetAllTasks()
        {
            return tasks;
        }

        public List<Task> GetAllTasksForWorker(int id)
        {
            List<Task> workerTasks = tasks.Where(t => t.AssigneeId == id).ToList();
            return workerTasks;
        }

        public Task GetTaskById(int id)
        {
            Task task = tasks.FirstOrDefault(t => t.Id == id);
            return task;
        }

        public bool CreateTask(Task task)
        {
            task.Id = GetNewTaskId(tasks);
            task.AssignedDate = DateTime.Now;
            try
            {
                tasks.Add(task);
                WriteJsonTaskData(tasks);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }

        // CRUD operations for Reports
        public List<Report> GetAllReports()
        {
            return reports;
        }

        public bool CreateReport(Report report, int managerId)
        {
            List<Worker> managers = GetAllManagers();
            Worker manager = managers.FirstOrDefault(m => m.Id == managerId);
            if (manager != null)
            {
                report.Id = GetNewReportId(GetAllReports());
                report.AssignedDate = DateTime.Now;
                // Add the new report to the manager's report log
                manager.ReportsIds.Add(report.Id);

                WriteReportJsonData(report);
                // Update the manager's data in the data storage
                WriteManagersJsonData(managers);

                return true;
            }
            return false;
        }

        public List<Report> GetAllReportsForManager(int managerId)
        {
            List<Report> managerReports = reports.Where(r => r.AssigneeId == managerId).ToList();
            return managerReports;
        }

        public Report GetReportById(int id)
        {
            Report report = reports.FirstOrDefault(t => t.Id == id);
            return report;
        }

        public List<Report> GetAllReportsForWorker(int workerId)
        {
            List<Report> workerReports = reports.Where(r => r.AssigneeId == workerId).ToList();
            return workerReports;
        }

        private int GetNewTaskId(List<Task> tasks)
        {
            Random random = new Random();
            int newId;
            bool idExists;
            do
            {
                newId = random.Next(1, 10000);
                idExists = tasks.Any(t => t.Id == newId);
            } while (idExists);

            return newId;
        }

        private int GetNewReportId(List<Report> reports)
        {
            Random random = new Random();
            int newId;
            bool idExists;
            do
            {
                newId = random.Next(1, 10000);
                idExists = reports.Any(t => t.Id == newId);
            } while (idExists);

            return newId;
        }
    }
}

public class JsonData
{
    public List<Worker> Workers { get; set; }
    public List<Task> Tasks { get; set; }
    public List<Report> Reports { get; set; }
}