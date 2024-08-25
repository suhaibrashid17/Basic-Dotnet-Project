using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SectionCourseRepository : ISectionCourseRepository
    {
        private readonly ApplicationDBContext _context;

        public SectionCourseRepository(ApplicationDBContext context)
        {
            _context = context;
        }

     public async Task<(bool isSuccess, string message)> AssignSectionToCourseAsync(string sectionName, string courseCode)
{
    var section = await _context.Section.FirstOrDefaultAsync(s => s.Name == sectionName);
    if (section == null)
    {
        return (false, $"Section '{sectionName}' not found."); // Section does not exist
    }

    var course = await _context.Course.FirstOrDefaultAsync(c => c.CourseCode == courseCode);
    if (course == null)
    {
        return (false, $"Course '{courseCode}' not found."); // Course does not exist
    }

    var existingPair = await _context.SectionCourse
        .FirstOrDefaultAsync(sc => sc.SectionId == section.Id && sc.CourseId == course.Id);

    if (existingPair != null)
    {
        return (false, $"The pair of Section '{sectionName}' and Course '{courseCode}' already exists."); // Pair already exists
    }

    var sectionCourse = new SectionCourse
    {
        SectionId = section.Id,
        CourseId = course.Id
    };

    _context.SectionCourse.Add(sectionCourse);
    await _context.SaveChangesAsync();

    return (true, $"Section '{sectionName}' successfully assigned to Course '{courseCode}'."); // Successfully assigned
}

    }
}
