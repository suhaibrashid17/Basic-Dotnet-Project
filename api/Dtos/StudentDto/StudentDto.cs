using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.StudentDto
{
    public class StudentDto
    {
         public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RollNumber { get; set; }
    }
}