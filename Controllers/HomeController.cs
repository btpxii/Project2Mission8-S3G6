using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project2Mission8_S3G6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
// Had to do this because "Task" is a reserved word in ASP.NET
using Tasks = Project2Mission8_S3G6.Models.Task;

namespace Project2Mission8_S3G6.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext taskContext { get; set; }
        // Construct HomeController with taskContext variable for sqlite db
        public HomeController(TaskContext tasks)
        {
            taskContext = tasks;
        }
        
        // Return home view
        public IActionResult Index()
        {
            return View();
        }

        // Display add task page, with categories passed in to construct the categories dropdown
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = taskContext.Categories.ToList();
            return View(new Tasks());
        }

        // Accept post requests from add tasks page, checks for validity before saving changes. If not valid, returns to get view with errors displayed
        [HttpPost]
        public IActionResult AddTask(Tasks task)
        {
            if (ModelState.IsValid)
            {
                taskContext.Add(task);
                taskContext.SaveChanges();
                return RedirectToAction("Quadrant");
            }
            else //if invalid
            {
                ViewBag.Categories = taskContext.Categories.ToList();
                return View();
            }
        }

        // Displays the quadrant view with all tasks in the database. Includes the categories table, joined on task.categoryId
        [HttpGet]
        public IActionResult Quadrant()
        {
            var taskList = taskContext.Responses.Include(x => x.Category).ToList();
            return View(taskList);
        }

        // Displays the Complete view, with the associated task passed in
        [HttpGet]
        public IActionResult Complete(int TaskId)
        {
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View(task);
        }

        // Accepts post requests from Complete view, completing the user's desired task
        [HttpPost]
        public IActionResult Complete(Tasks task)
        {
            task = taskContext.Responses.Single(x => x.TaskId == task.TaskId);
            task.Completed = true;
            taskContext.Update(task);
            taskContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        // Displays the Edit view, passing in the selected task
        [HttpGet]
        public IActionResult Edit (int TaskId)
        {
            ViewBag.Categories = taskContext.Categories.ToList();
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View("AddTask", task);

        }

        // Accepts post requests from the edit view, allowing a user to save their changes to a given task
        [HttpPost]
        public IActionResult Edit(Tasks task)
        {
            taskContext.Update(task);
            taskContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        // Displays the delete page view, passing in the selected task
        [HttpGet]
        public IActionResult Delete(int TaskId)
        {
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View(task);
        }

        // Accepts post requests from the Delete page view, allowing a user to delete a selected task
        [HttpPost]
        public IActionResult Delete(Tasks task)
        {
            taskContext.Responses.Remove(task);
            taskContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
