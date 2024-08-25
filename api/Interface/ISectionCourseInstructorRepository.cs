using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface ISectionCourseInstructorRepository
    {
        Task<(bool isSuccess, string message, SectionCourseInstructor? sectionCourseInstructor)> AssignInstructorToSectionCourseAsync(string sectionName, string courseCode, string instructorUsername);
    }
}