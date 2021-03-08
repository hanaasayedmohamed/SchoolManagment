using ApiWithbasicAuthentication.Common;
using ApiWithbasicAuthentication.Domain.Model;
using ApiWithbasicAuthentication.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.Services
{
    public class StudentService : IStudentService
    {
        private readonly FirstCoreAppDbContext _dbContext;

        public StudentService(FirstCoreAppDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        public PagedList<Student> GetStudents(StudentParameters pagingParameter)
        {
            var students = FindAll();

            SearchByName(ref students, pagingParameter.GradeName);

            return PagedList<Student>.ToPagedList(students.OrderBy(on => on.StudentName),
        pagingParameter.PageNumber,
        pagingParameter.PageSize);

        }

        public IQueryable<Student> FindAll()
        {
            return _dbContext.Students.Include(C => C.Grade).AsQueryable();
        }

        private void SearchByName(ref IQueryable<Student> students, string gradeName)
        {
            if (!students.Any() || string.IsNullOrWhiteSpace(gradeName))
                return;
            students = students.Where(o => o.Grade.GradeName.ToLower().Contains(gradeName.Trim().ToLower()));
        }

        public Student InsertStudent(Student student)
        {
            if (IsStudentNameUnique(student.StudentName))
            {
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return student;
            }
            else
            {
                throw new CustomException("Error_409" , ResponseErrorMessage.UniqueRuleViolationMessage);
            }
        }

        public bool IsStudentNameUnique(string StudentName)
        {
            var student = FindAll().Where(C => C.StudentName == StudentName).FirstOrDefault();
            return (student == null) ? true : false;
        }
    }
}
