using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project2Mission8_S3G6.Models
{
    // Builds the task class, with the category class linked as a foreign key
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set;}
        [Required]
        public string TaskName { get; set; }
        [DataType(DataType.Date)]
        public string DueDate { get; set; }
        [Required]
        public int Quadrant { get; set; }
        public bool Completed { get; set; }
        //foreign key
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
