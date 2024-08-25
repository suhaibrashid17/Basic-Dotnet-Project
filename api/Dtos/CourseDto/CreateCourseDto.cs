using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CourseDto
{
    public class CreateCourseDto
    {
        public string CourseCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; 
    }
}