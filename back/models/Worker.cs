namespace dev_ex.models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int ManagerId { get; set; }
        public List<int> EmployeesIds { get; set; }
        public List<int> ReportsIds { get; set; }
    }
}