using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.SectionCourseStudentDto
{
    public class RegisterStudentDto
    {
        public string SectionName { get; set; }
        public string CourseCode { get; set; }
        public string StudentUsername { get; set; }

    }
}