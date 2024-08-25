using System.Threading.Tasks;
using api.Data;
using api.Interface;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SectionCourseInstructorRepository : ISectionCourseInstructorRepository
{
    private readonly ApplicationDBContext _context;

    public SectionCourseInstructorRepository(ApplicationDBContext context)
    {
        _context = context;
    }

 public async Task<(bool isSuccess, string message, SectionCourseInstructor? sectionCourseInstructor)> AssignInstructorToSectionCourseAsync(string sectionName, string courseCode, string instructorUsername)
{
    // Find the SectionCourse pair
    var sectionCourse = await _context.SectionCourse
        .Include(sc => sc.Section)
        .Include(sc => sc.Course)
        .FirstOrDefaultAsync(sc => sc.Section.Name == sectionName && sc.Course.CourseCode == courseCode);

    if (sectionCourse == null)
    {
        return (false, "Section-Course pair does not exist.", null); // Section-Course pair does not exist
    }

    // Find the instructor by username
    var instructor = await _context.Instructor
        .FirstOrDefaultAsync(i => i.UserName == instructorUsername);

    if (instructor == null)
    {
        return (false, "Instructor not found.", null); // Instructor not found
    }

    // Check if the instructor is already assigned to this section-course pair
    var existingAssignment = await _context.SectionCourseInstructor
        .FirstOrDefaultAsync(sci => sci.SectionId == sectionCourse.SectionId && sci.CourseId == sectionCourse.CourseId && sci.InstructorId == instructor.Id);

    if (existingAssignment != null)
    {
        return (false, "This instructor is already assigned to this section-course pair.", null); // Instructor already assigned
    }

    var sectionCourseInstructor = new SectionCourseInstructor
    {
        SectionId = sectionCourse.SectionId,
        CourseId = sectionCourse.CourseId,
        InstructorId = instructor.Id
    };

    _context.SectionCourseInstructor.Add(sectionCourseInstructor);
    await _context.SaveChangesAsync();

    return (true, "Instructor successfully assigned to Section-Course pair.", sectionCourseInstructor); // Successfully assigned
}

}

}