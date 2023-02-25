using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project2Mission8_S3G6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task = Project2Mission8_S3G6.Models.Task;

namespace Project2Mission8_S3G6.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext blahContext { get; set; }

        public HomeController(TaskContext someName)
        {
            blahContext = someName;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TaskInput()
        {
            ViewBag.Categories = blahContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult TaskInput(Task Response)
        {
            if (ModelState.IsValid)
            {
                blahContext.Add(Response);
                blahContext.SaveChanges();
                return View("Quadrant", Response);
            }
            else //if invalid
            {
                ViewBag.Categories = blahContext.Categories.ToList();
                return View();
            }
        }
        [HttpGet]
        public IActionResult Quadrant()
        {
            var taskList = blahContext.Responses.Include(x => x.Category);
            return View(taskList);
        }
        [HttpGet]
        public IActionResult Edit(int TaskId)
        {
            ViewBag.Categories = blahContext.Categories.ToList();
            var task = blahContext.Responses.Single(x => x.TaskId == TaskId);
            return View("TaskInput", task);

        }

        public IActionResult Edit(Task blah)
        {
            blahContext.Update(blah);
            blahContext.SaveChanges();
            return RedirectToAction("Quadrant");
        }
        [HttpGet]
        public IActionResult Delete(int TaskId)
        {
            var task = blahContext.Responses.Single(x => x.TaskId == TaskId);
            return View(task);
        }
        [HttpPost]
        public IActionResult Delete(Task Response)
        {
            blahContext.Responses.Remove(Response);
            blahContext.SaveChanges();
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
