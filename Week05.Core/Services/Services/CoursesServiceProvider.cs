using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates;
		private readonly IRepository<Person> _persons;

		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

		/// <summary>
		/// You should implement this function, such that all tests will pass.
		/// </summary>
		/// <param name="courseInstanceID">The ID of the course instance which the teacher will be registered to.</param>
		/// <param name="model">The data which indicates which person should be added as a teacher, and in what role.</param>
		/// <returns>Should return basic information about the person.</returns>
		public PersonDTO AddTeacherToCourse(int courseInstanceID, AddTeacherViewModel model)
		{
			// TODO: implement this logic!

      var course = (from c in _courseInstances.All()
        where c.ID == courseInstanceID
        select c).SingleOrDefault();
      if(course == null)
      {
          throw new AppObjectNotFoundException();
      }
      var teacher = (from p in _persons.All()
        where p.SSN == model.SSN
        select p).SingleOrDefault();
      if(teacher == null)
      {
          throw new AppObjectNotFoundException();
      }

      var mainTeacher = (from tr in _teacherRegistrations.All()
        where tr.Type == TeacherType.MainTeacher && tr.CourseInstanceID == courseInstanceID
        select tr).SingleOrDefault();
      var alreadyRegistered = (from tr in _teacherRegistrations.All()
        where tr.CourseInstanceID == courseInstanceID && tr.SSN == model.SSN
        select tr).SingleOrDefault();

      if(alreadyRegistered != null)
      {
          throw new AppValidationException("PERSON_ALREADY_REGISTERED_TEACHER_IN_COURSE");
      }
      if(alreadyRegistered == null && mainTeacher != null)
      {
          throw new AppValidationException("COURSE_ALREADY_HAS_A_MAIN_TEACHER");
      }

      var teacherRegistration = new TeacherRegistration
      {
          SSN = model.SSN,
          CourseInstanceID = courseInstanceID,
          Type = model.Type
      };
      _teacherRegistrations.Add(teacherRegistration);
      _uow.Save();

      var person = (from p in _persons.All() where p.SSN == model.SSN select p).SingleOrDefault();
      return new PersonDTO{SSN = person.SSN, Name = person.Name};

		}

		/// <summary>
		/// You should write tests for this function. You will also need to
		/// modify it, such that it will correctly return the name of the main
		/// teacher of each course.
		/// </summary>
		/// <param name="semester"></param>
		/// <returns></returns>
		public List<CourseInstanceDTO> GetCourseInstancesBySemester(string semester = null)
		{
			if (string.IsNullOrEmpty(semester))
			{
				semester = "20153";
			}

			var courses = (from c in _courseInstances.All()
				join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
				where c.SemesterID == semester
				select new CourseInstanceDTO
				{
					Name               = ct.Name,
					TemplateID         = ct.CourseID,
					CourseInstanceID   = c.ID,
					MainTeacher        = 
				}).ToList();

			return courses;
		}
	}
}
