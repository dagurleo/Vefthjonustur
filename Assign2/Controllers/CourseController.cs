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
                         EndDate    = DateTime.Now.AddMonths(3),
                         Students = new List<Student>{
                             new Student {
                                 SSN = "1234567891",
                                 Name = "Gudjon Orri Helgason"
                             },
                             new Student {
                                 SSN = "9876543211",
                                 Name = "Theodor Agust Magnusson"
                             }
                         }
                     },
                     new Course
                     {
                         ID         = 2,
                         Name       = "Web Programming 2",
                         TemplateID = "T-1337-WebP",
                         StartDate  = DateTime.Now,
                         EndDate    = DateTime.Now.AddMonths(3),
                         Students = new List<Student>{
                             new Student {
                                 SSN = "2305932909",
                                 Name = "Snorri Mar Skulason"
                             },
                             new Student {
                                 SSN = "2309912349",
                                 Name = "Gundi Gudmundsson"
                             }
                         }
                     },
                };
            }
        }

        //GET REQUESTS



        /// <summary>
        ///   This method returns all courses
        /// </summary>

        [HttpGet]
        [Route("all")]
        public IEnumerable<Course> GetCourses(){
            return _courses;
        }

        /// <summary>
        ///   This method returns a course based on the id provied
        /// </summary>
        [HttpGet]
        [Route("{courseInstanceID:int}")]
        public IActionResult GetCourseById(int courseInstanceID){
            return new ObjectResult(_courses.Single(c => c.ID == courseInstanceID));
        }

        /// <summary>
        ///    This method returns all courses in a particular course
        /// </summary>
        [HttpGet]
        [Route("{courseInstanceID:int}/students")]
        public IEnumerable<Student> GetStudentsByCourse(int courseInstanceID){
            if(courseInstanceID >= _courses.Count){
              return null;
            }
            return _courses.Single(c => c.ID == courseInstanceID).Students;

        }

        //POST REQUESTS

        /// <summary>
        ///   This method creates a new course
        /// </summary>
        [HttpPost]
        [Route("create")]
        public IActionResult CreateCourse(Course course){
            _courses.Add(course);
            return Created("create", course);
        }
        /// <summary>
        ///   This method creates a new student in a particular course
        /// </summary>
        [HttpPost]
        [Route("{courseInstanceID:int}/students")]
        public IActionResult AddStudentToCourse(int courseInstanceID, Student student){
            _courses[courseInstanceID - 1].Students.Add(student);
            return Created("create", student);
        }

        //PUT REQUESTS(UPDATE)

        /// <summary>
        ///   This method updates a particular course
        /// </summary>
        [HttpPut]
        [Route("{courseInstanceID:int}")]
        public IActionResult UpdateCourse(int courseInstanceID, Course course){

            _courses[courseInstanceID - 1] = course;

            _courses[courseInstanceID - 1].ID = courseInstanceID;
            return new NoContentResult();
        }
        //DELETE REQUESTS

        /// <summary>
        ///   This method deletes a course from the system
        /// </summary>
        [HttpDelete]
        [Route("{courseInstanceID:int}")]
        public IActionResult DeleteCourse(int courseInstanceID){

            _courses.Remove(_courses.Single(c => c.ID == courseInstanceID));
            return new NoContentResult();
        }

    }
}
