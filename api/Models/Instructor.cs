using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Instructor:UserBase
    {
        public List<SectionCourseInstructor> SectionCourseInstructors { get; set; } = new List<SectionCourseInstructor>();
    }
}