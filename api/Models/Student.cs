using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Student:UserBase
    {
        public string RollNumber { get; set; } = string.Empty;
        public List<SectionCourseStudent> SectionCourseStudents { get; set; } = new List<SectionCourseStudent>();

    }
}