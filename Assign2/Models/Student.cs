using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication.Models{
  /// <summary>
  ///   This model is for the student
  /// </summary>
    public class Student{
        public string SSN {get; set;}
        public string Name {get; set;}
    }
}
