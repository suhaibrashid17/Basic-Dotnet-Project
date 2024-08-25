using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<UserBase>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }
        public DbSet<Admin> Admin {get;set;}
        public DbSet<Course> Course {get;set;}

        public DbSet<Instructor> Instructor {get;set;}
        public DbSet<Section> Section {get;set;}
        public DbSet<SectionCourse> SectionCourse {get;set;}
        public DbSet<SectionCourseInstructor> SectionCourseInstructor {get;set;}
        public DbSet<SectionCourseStudent> SectionCourseStudent {get;set;}

        public DbSet<Student> Student {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<SectionCourse>(x=>x.HasKey(sc=>new{sc.SectionId,sc.CourseId}));
            builder.Entity<SectionCourse>()
            .HasOne(s=>s.Section)
            .WithMany(sc=>sc.SectionCourses)
            .HasForeignKey(s=>s.SectionId);
            builder.Entity<SectionCourse>()
            .HasOne(c=>c.Course)
            .WithMany(sc=>sc.SectionCourse)
            .HasForeignKey(c=>c.CourseId);

            builder.Entity<SectionCourseInstructor>(x=>x.HasKey(sc=>new{sc.SectionId,sc.CourseId,sc.InstructorId}));
            builder.Entity<SectionCourseInstructor>()
            .HasOne(i=>i.Instructor)
            .WithMany(sci=>sci.SectionCourseInstructors)
            .HasForeignKey(i=>i.InstructorId);
            builder.Entity<SectionCourseInstructor>()
            .HasOne(sc=>sc.SectionCourse)
            .WithMany(sci=>sci.SectionCourseInstructors)
            .HasForeignKey(sc=>new{sc.SectionId,sc.CourseId});

             builder.Entity<SectionCourseStudent>(x=>x.HasKey(sc=>new{sc.SectionId,sc.CourseId,sc.StudentId}));
            builder.Entity<SectionCourseStudent>()
            .HasOne(s=>s.Student)
            .WithMany(scs=>scs.SectionCourseStudents)
            .HasForeignKey(s=>s.StudentId);
            builder.Entity<SectionCourseStudent>()
            .HasOne(sc=>sc.SectionCourse)
            .WithMany(sci=>sci.SectionCourseStudents)
            .HasForeignKey(sc=>new{sc.SectionId,sc.CourseId});


         List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Instructor",
                    NormalizedName = "INSTRUCTOR"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
       
        }


    }
}