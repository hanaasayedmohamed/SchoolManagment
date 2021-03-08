using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApiWithbasicAuthentication.Domain.Model
{
    public class StudentDetail
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }
    }

}
