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

        internal List<TaskModel>? CompleteTask(string ID, List<TaskModel>? tasks)
        {
            foreach (var task in tasks)
            {
                if(task.Id == ID)
                {
                    task.IsActive = false;
                    break;
                }
            }
            return tasks;
        }

        internal List<TaskModel>? DeleteTask(string ID, List<TaskModel>? tasks)
        {
            TaskModel task = tasks.Find(t => t.Id == ID.ToString());
            tasks.Remove(task);
            return tasks;
        }
    }
}
