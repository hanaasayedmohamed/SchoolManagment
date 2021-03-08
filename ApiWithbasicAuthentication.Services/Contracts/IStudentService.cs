using ApiWithbasicAuthentication.Common;
using ApiWithbasicAuthentication.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithbasicAuthentication.Services
{
   public interface IStudentService
    {
        PagedList<Student> GetStudents(StudentParameters pagingParameter);
        Student InsertStudent(Student student);
    }
}
