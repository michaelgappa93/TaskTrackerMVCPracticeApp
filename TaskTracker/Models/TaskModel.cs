namespace TaskTracker.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusType Status { get; set; }        
    }

    public enum StatusType
    {
        Assigned,
        Pending,
        Completed,
        Unassigned
    }
}
