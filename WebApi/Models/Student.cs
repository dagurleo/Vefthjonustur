using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication.Models{
    public class Student{
        public string SSN {get; set;}
        public string Name {get; set;}
    }
}
