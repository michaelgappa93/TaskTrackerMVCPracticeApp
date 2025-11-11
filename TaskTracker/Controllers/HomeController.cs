using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskService _taskService;
        private readonly ILogger<HomeController> _logger;
        public const string SessionKeyTasks = "_Task";

        public HomeController(ILogger<HomeController> logger, TaskService taskService)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public IActionResult Index(TaskHomePageModel taskHomePage)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                return View(taskHomePage);
            }
            taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks)); 
            return View(taskHomePage);
        }

        [HttpPost]
        public IActionResult AddTask(TaskHomePageModel taskHomePage)
        {
            
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                TaskModel task = new TaskModel
                {
                    Title = taskHomePage.TaskName,
                    Description = taskHomePage.TaskDescription,
                    DueDate = taskHomePage.DueDate,
                    Status = StatusType.Unassigned,
                    Id = 0
                };
                taskHomePage.Tasks = _taskService.CreateAndAddTask(task, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
            }
            else
            {
                
                taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks));
                TaskModel task = new TaskModel
                {
                    Title = taskHomePage.TaskName,
                    Description = taskHomePage.TaskDescription,
                    DueDate = taskHomePage.DueDate,
                    Status = StatusType.Unassigned,
                    Id = taskHomePage.Tasks.Count
                };
                taskHomePage.Tasks = _taskService.CreateAndAddTask(task, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
            }
            
            return View("Index", taskHomePage);
        }

        [HttpPost]
        public IActionResult DeleteTask(TaskHomePageModel taskHomePage)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks));
                taskHomePage.Tasks = _taskService.DeleteTask(taskHomePage.TaskId, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
                return View("Index", taskHomePage);
            }
            return View("Index", taskHomePage);
        }

        [HttpPost]
        public IActionResult CompleteTask(TaskHomePageModel taskHomePage)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks));
                taskHomePage.Tasks = _taskService.CompleteTask(taskHomePage.TaskId, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
                return View("Index", taskHomePage);
            }
            return View("Index", taskHomePage);
        }

        [HttpPost]
        public IActionResult Sort(TaskHomePageModel taskHomePage)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                return View("Index", taskHomePage);
            }
            
            taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks));
            if (taskHomePage.sortByAscending != "True")
            {
                taskHomePage.Tasks = _taskService.SortTasks(taskHomePage.Tasks, taskHomePage.sortByAscending);
                taskHomePage.sortByAscending = "True";
            }
            else
            {
                taskHomePage.Tasks = _taskService.SortTasks(taskHomePage.Tasks, taskHomePage.sortByAscending);
                taskHomePage.sortByAscending = "False";
            }
            return View("Index", taskHomePage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
