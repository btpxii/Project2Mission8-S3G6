using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project2Mission8_S3G6.Models
{
    public class TaskContext :DbContext
    {
        //constructor
        public TaskContext(DbContextOptions<TaskContext> options) : base (options)
        {
           
        }

        public DbSet<Task> Responses { get; set;}
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Home" },
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
                );


            mb.Entity<Task>().HasData(
                new Task
                {
                    TaskId = 1,
                    TaskName = "Do dishes",
                    Quadrant = 3,
                    Completed = false,
                    CategoryId =1
                },
                new Task
                {
                    TaskId = 2,
                    TaskName = "Clean bathroom",
                    Quadrant = 1,
                    Completed = true,
                    CategoryId = 1
                },
                new Task
                {
                    TaskId = 3,
                    TaskName = "Ministering",
                    Quadrant = 2,
                    Completed = false,
                    CategoryId = 4
                }
                );
        }
    }
}
