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

        internal List<TaskModel>? DeleteTask(int ID, List<TaskModel>? tasks)
        {
            TaskModel task = tasks.Find(t => t.Id == ID);
            tasks.Remove(task);
            return tasks;
        }
    }
}
