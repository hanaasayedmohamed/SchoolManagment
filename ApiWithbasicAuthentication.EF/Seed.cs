using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiWithbasicAuthentication.Domain.Model;

namespace ApiWithbasicAuthentication.EF
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FirstCoreAppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FirstCoreAppDbContext>>()))
            {
                // Look for any students and Grades.
                if (context.Students.Any() && context.Grades.Any())
                {
                    return;   // DB has been seeded
                }
                context.Grades.AddRange(
                        new Grade() { GradeName = "First grade", Section = "information system 1" },
                        new Grade() { GradeName = "2 grade", Section = "information system 2" },
                        new Grade() { GradeName = "3 grade", Section = "information system 3" },
                        new Grade() { GradeName = "4 grade",Section = "information system 4" },
                        new Grade() { GradeName = "5 grade", Section = "information system 5" },
                        new Grade() { GradeName = "6 grade", Section = "information system 6" },
                        new Grade() { GradeName = "7 grade",  Section = "information system 7" },
                        new Grade() { GradeName = "8 grade", Section = "information system 8" }

                    );

                context.SaveChanges();


                context.Students.AddRange(
                    new Student
                    {
                        StudentName = "Hana",
                        Height = 166,
                        Weight = 80,
                        DateOfBirth = DateTime.Now.AddYears(-30),
                        GradeID = 9
                    }
                    , new Student
                    {
                        StudentName = "mohamed",
                        Height = 166,
                        Weight = 80,
                        DateOfBirth = DateTime.Now.AddYears(-28),
                        GradeID = 10
                    }
                     , new Student
                     {
                         StudentName = "Amira",
                         Height = 166,
                         Weight = 80,
                         DateOfBirth = DateTime.Now.AddYears(-24),
                         GradeID = 10
                     }
                       , new Student
                       {
                           StudentName = "Hala",
                           Height = 166,
                           Weight = 80,
                           DateOfBirth = DateTime.Now.AddYears(-22),
                           GradeID = 9
                       }
                       , new Student
                       {
                           StudentName = "Hayam",
                           Height = 166,
                           Weight = 80,
                           DateOfBirth = DateTime.Now.AddYears(-18),
                           GradeID = 11
                       }
                       , new Student
                       {
                           StudentName = "Reham",
                           Height = 166,
                           Weight = 80,
                           DateOfBirth = DateTime.Now.AddYears(-14),
                           GradeID = 11
                       }
                        , new Student
                        {
                            StudentName = "nady",
                            Height = 166,
                            Weight = 80,
                            DateOfBirth = DateTime.Now.AddYears(-12),
                            GradeID = 12
                        }
                         , new Student
                         {
                             StudentName = "Youman",
                             Height = 166,
                             Weight = 80,
                             DateOfBirth = DateTime.Now.AddYears(-2),
                             GradeID = 12
                         }
                );
                context.SaveChanges();
            }
        }
    }
}
