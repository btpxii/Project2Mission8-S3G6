using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project2Mission8_S3G6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tasks = Project2Mission8_S3G6.Models.Task;

namespace Project2Mission8_S3G6.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext taskContext { get; set; }

        public HomeController(TaskContext tasks)
        {
            taskContext = tasks;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Categories = taskContext.Categories.ToList();
            return View(new Tasks());
        }

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

        [HttpGet]
        public IActionResult Quadrant()
        {
            var taskList = taskContext.Responses.Include(x => x.Category).ToList();
            return View(taskList);
        }

        [HttpGet]
        public IActionResult Complete(int TaskId)
        {
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View(task);
        }

        [HttpPost]
        public IActionResult Complete(Tasks task)
        {
            task = taskContext.Responses.Single(x => x.TaskId == task.TaskId);
            task.Completed = true;
            taskContext.Update(task);
            taskContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult Edit (int TaskId)
        {
            ViewBag.Categories = taskContext.Categories.ToList();
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View("AddTask", task);

        }

        [HttpPost]
        public IActionResult Edit(Tasks task)
        {
            taskContext.Update(task);
            taskContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }

        [HttpGet]
        public IActionResult Delete(int TaskId)
        {
            var task = taskContext.Responses.Single(x => x.TaskId == TaskId);
            return View(task);
        }

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
