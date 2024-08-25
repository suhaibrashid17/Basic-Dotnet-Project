using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.SectionCourseInstructorDto
{
    public class AssignInstructorDto
    {
        public string SectionName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorUsername { get; set; }
    }
}