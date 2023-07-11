namespace dev_ex.models
{
    public class Task
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AssigneeId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }

    }
}