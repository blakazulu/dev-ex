using dev_ex.data;
using dev_ex.models;
using Newtonsoft.Json;
using Task = dev_ex.models.Task;

var builder = WebApplication.CreateBuilder(args);

// Add configuration setup
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
// Add the DataAccess service as a singleton
builder.Services.AddSingleton<DataAccess>(provider =>
{
    var jsonFilesPath = configuration["Data:JsonFilesPath"];
    return new DataAccess(jsonFilesPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200") // Replace with your Angular app URL
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Generate sample data
// app.Run(async context =>
// {
//     var managers = GenerateManagers();
//     var employees = GenerateEmployees(managers);
//     var tasks = GenerateTasks(employees, managers);
//     var reports = GenerateReports(managers);

//     var jsonData = new JsonData
//     {
//         Workers = employees.Cast<Worker>().Concat(managers).ToList(),
//         Tasks = tasks,
//         Reports = reports
//     };

//     var jsonDataString = JsonConvert.SerializeObject(jsonData);

//     var jsonFilesPath = configuration["Data:JsonFilesPath"];
//     File.WriteAllText(jsonFilesPath, jsonDataString);

//     await context.Response.WriteAsync("Sample data generated and saved to JSON file.");
// });

List<Worker> GenerateManagers()
{
    List<Worker> managers = new List<Worker>();
    Random random = new Random();

    // Generate 8 managers
    for (int i = 1; i <= 8; i++)
    {
        string firstName = GenerateRandomName(true);
        string lastName = GenerateRandomName(false);
        string position = GenerateRandomPosition();
        List<int> employeesIds = new List<int>();
        List<int> reportsIds = new List<int>();

        managers.Add(new Worker
        {
            Id = i,
            FirstName = firstName,
            LastName = lastName,
            Position = position,
            ManagerId = 1, // No manager for the first manager
            EmployeesIds = employeesIds,
            ReportsIds = reportsIds
        });
    }

    return managers;
}

List<Worker> GenerateEmployees(List<Worker> managers)
{
    List<Worker> employees = new List<Worker>();
    Random random = new Random();

    // Generate 32 employees
    for (int i = 9; i <= 40; i++)
    {
        string firstName = GenerateRandomName(true);
        string lastName = GenerateRandomName(false);
        string position = GenerateRandomPosition();
        int managerId = managers[random.Next(0, managers.Count)].Id;
        List<int> employeesIds = new List<int>();
        List<int> reportsIds = new List<int>();

        employees.Add(new Worker
        {
            Id = i,
            FirstName = firstName,
            LastName = lastName,
            Position = position,
            ManagerId = managerId,
            EmployeesIds = employeesIds,
            ReportsIds = reportsIds
        });

        // Add employee to manager's subordinates
        Worker manager = managers.FirstOrDefault(m => m.Id == managerId);
        manager?.EmployeesIds.Add(i);
    }

    return employees;
}

List<Task> GenerateTasks(List<Worker> employees, List<Worker> managers)
{
    List<Task> tasks = new List<Task>();
    Random random = new Random();

    foreach (var employee in employees)
    {
        int numTasks = random.Next(3, 7); // Generate 3-6 tasks for each employee

        for (int i = 1; i <= numTasks; i++)
        {
            string taskText = GenerateRandomText();
            int assigneeId = employee.Id;
            DateTime assignedDate = DateTime.Now;
            DateTime dueDate = DateTime.Now.AddDays(random.Next(1, 30));

            tasks.Add(new Task
            {
                Id = tasks.Count + 1,
                Text = taskText,
                AssigneeId = assigneeId,
                AssignedDate = assignedDate,
                DueDate = dueDate
            });
        }
    }

    return tasks;
}

List<Report> GenerateReports(List<Worker> managers)
{
    List<Report> reports = new List<Report>();
    Random random = new Random();

    foreach (var manager in managers)
    {
        foreach (var employeeId in manager.EmployeesIds)
        {
            int numReports = random.Next(3, 7); // Generate 3-6 reports from each employee to each manager

            for (int i = 1; i <= numReports; i++)
            {
                string reportText = GenerateRandomText();
                int assigneeId = manager.Id;
                DateTime assignedDate = DateTime.Now;

                reports.Add(new Report
                {
                    Id = reports.Count + 1,
                    Text = reportText,
                    AssigneeId = assigneeId,
                    AssignedDate = assignedDate
                });
            }
        }
    }

    return reports;
}

string GenerateRandomName(bool isFirst)
{
    string[] firstNames = { "John", "Emily", "Michael", "Olivia", "William", "Sophia", "James", "Emma", "Benjamin", "Isabella", "Daniel", "Ava", "David", "Mia", "Alexander", "Charlotte", "Matthew", "Amelia", "Joseph", "Harper" };
    string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin" };

    Random random = new Random();
    string firstName = firstNames[random.Next(0, firstNames.Length)];
    string lastName = lastNames[random.Next(0, lastNames.Length)];

    return isFirst ? firstName : lastName;
}

string GenerateRandomPosition()
{
    string[] positions = {
        "UI Designer",
        "Backend Developer",
        "Data Analyst",
        "Network Engineer",
        "Scrum Master",
        "IT Consultant",
        "Systems Administrator",
        "Business Analyst",
        "Database Administrator",
        "Mobile App Developer",
        "Technical Writer",
        "Artificial Intelligence Specialist",
        "Cloud Solutions Architect",
        "IT Security Analyst",
        "IT Support Specialist"
    };

    Random random = new Random();
    return positions[random.Next(0, positions.Length)];
}

string GenerateRandomText()
{
    string[] texts = { "Lorem ipsum dolor sit amet.", "Consectetur adipiscing elit.", "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "Ut enim ad minim veniam.", "Quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", "Excepteur sint occaecat cupidatat non proident.", "Sunt in culpa qui officia deserunt mollit anim id est laborum." };

    Random random = new Random();
    return texts[random.Next(0, texts.Length)];
}

app.MapControllers();

app.Run();
