using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("courses")]
    public class CourseController : Controller {
        private static List<Course> _courses;
        public CourseController(){
            if(_courses == null ){
                _courses = new List<Course> {
                    new Course
                     {
                         ID         = 1,
                         Name       = "Web services",
                         TemplateID = "T-514-VEFT",
                         StartDate  = DateTime.Now,
                         EndDate    = DateTime.Now.AddMonths(3)
                     },
                     new Course
                     {
                         ID         = 2,
                         Name       = "Web Programming 2",
                         TemplateID = "T-1337-WebP",
                         StartDate  = DateTime.Now,
                         EndDate    = DateTime.Now.AddMonths(3)
                     },
                };
            }
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<Course> GetCourses(){
            return _courses;
        }
        [HttpPost]
        [Route("create")]
        public IActionResult CreateCourse([FromBody] Course course){
            _courses.Add(course);
            return Created("create", course);
        }
    }
}
