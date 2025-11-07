using Microsoft.VisualBasic;
using System.Threading.Tasks;
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

        internal List<TaskModel>? CompleteTask(int ID, List<TaskModel>? tasks)
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

        internal List<TaskModel>? DeleteTask(int ID, List<TaskModel>? tasks)
        {
            TaskModel task = tasks.Find(t => t.Id == ID);
            tasks.Remove(task);
            return tasks;
        }

        internal List<TaskModel> SortTasks(List<TaskModel> tasks, string sortDirection)
        {
            if (sortDirection == "True")
            {
                tasks = tasks.OrderBy(t => t.DueDate).ToList();
                return tasks;
            }
            else
            {
                tasks = tasks.OrderByDescending(t => t.DueDate).ToList();
                return tasks;
            }
                
            
            
            
        }
    }
}
