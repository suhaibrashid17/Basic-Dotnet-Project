using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helper
{
    public class SearchCriteriaAdmin
    {
        public bool IncludeSection { get; set; }
    public bool IncludeCourse { get; set; }
    public bool IncludeStudent { get; set; }
    public bool IncludeInstructor { get; set; }

    public string? SectionName { get; set; }
    public string? CourseCode { get; set; }
    public string? StudentUsername { get; set; }
    public string? InstructorUsername { get; set; }
    }
}