using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskService _taskService;
        private readonly ILogger<HomeController> _logger;
        public const string SessionKeyTasks = "_Task";

        public TaskHomePageModel taskHomePage = new TaskHomePageModel();

        public HomeController(ILogger<HomeController> logger, TaskService taskService)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public IActionResult Index(TaskHomePageModel taskHomePage)
        {
            return View(taskHomePage);
        }
        [HttpPost]
        public IActionResult AddTask(TaskHomePageModel taskHomePage)
        {
            TaskModel task = new TaskModel
            {
                Title = taskHomePage.TaskName,
                Description = taskHomePage.TaskDescription,
                DueDate = taskHomePage.DueDate
            };
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyTasks)))
            {
                taskHomePage.Tasks = _taskService.CreateAndAddTask(task, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
            }
            else
            {
                taskHomePage.Tasks = JsonSerializer.Deserialize<List<TaskModel>>(HttpContext.Session.GetString(SessionKeyTasks));
                taskHomePage.Tasks = _taskService.CreateAndAddTask(task, taskHomePage.Tasks);
                HttpContext.Session.SetString(SessionKeyTasks, JsonSerializer.Serialize(taskHomePage.Tasks));
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
