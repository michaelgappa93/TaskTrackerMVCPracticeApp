using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskService
    {
        public List<TaskModel> CreateAndAddTask(TaskModel task, List<TaskModel> tasks)
        {
            tasks.Add(task);
            return tasks;
        }
    }
}
