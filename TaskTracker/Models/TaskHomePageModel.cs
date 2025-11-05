namespace TaskTracker.Models
{
    public class TaskHomePageModel
    {
        public List<TaskModel> Tasks = new List<TaskModel>();
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}
