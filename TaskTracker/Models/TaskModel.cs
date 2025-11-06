namespace TaskTracker.Models
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public bool IsActive { get; set; }
    }
}
