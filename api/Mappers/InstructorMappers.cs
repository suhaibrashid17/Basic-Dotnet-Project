using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.InstructorDto;
using api.Models;

namespace api.Mappers
{
    public static class InstructorMappers
    {
        public static InstructorDto ToInstructorDto(this Instructor instructor)
        {
            return new InstructorDto{
                Name = instructor.Name,
                UserName = instructor.UserName,
                Email = instructor.Email
            };
        }
        public static Instructor ToInstructorFromCreate(this CreateInstructorDto createInstructorDto)
        {
            return new Instructor{
                Name = createInstructorDto.Name,
                UserName = createInstructorDto.UserName,
                Email = createInstructorDto.Email
            };
        }
    }
}