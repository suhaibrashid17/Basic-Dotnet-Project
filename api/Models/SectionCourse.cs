using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class SectionCourse
    {
        public int SectionId { get; set; }
        public Section? Section { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public List<SectionCourseInstructor> SectionCourseInstructors { get; set; } = new List<SectionCourseInstructor>();
        public List<SectionCourseStudent> SectionCourseStudents { get; set; } = new List<SectionCourseStudent>();


        
    }
}