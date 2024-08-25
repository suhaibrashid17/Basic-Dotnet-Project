using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface ISectionCourseStudentRepository
    {
Task<(bool isSuccess, string message, SectionCourseStudent? sectionCourseStudent)> RegisterStudentToSectionCourseAsync(string sectionName, string courseCode, string studentUsername);
    }
}