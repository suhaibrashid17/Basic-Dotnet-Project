using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SectionCourseInstructor
    {
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public SectionCourse? SectionCourse{ get; set; }
        public string InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}