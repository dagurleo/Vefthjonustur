using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication.Models{
    public class Course {
        public string Name {get; set;}
        public string TemplateID {get; set;}
        public int ID {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public List<Student> Students {get; set;}
    }
}
