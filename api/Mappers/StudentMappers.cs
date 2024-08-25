using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StudentDto;
using api.Models;

namespace api.Mappers
{
    public static class StudentMappers
    {
          public static StudentDto ToStudentDto(this Student student)
        {
            return new StudentDto{
                Name = student.Name,
                UserName = student.UserName,
                Email = student.Email,
                RollNumber = student.RollNumber
            };
        }
        public static Student ToStudentFromCreate(this CreateStudentDto createStudentDto)
        {
            return new Student{
                Name = createStudentDto.Name,
                UserName = createStudentDto.UserName,
                Email = createStudentDto.Email,
                RollNumber = createStudentDto.RollNumber
            };
        }
        
    }
}