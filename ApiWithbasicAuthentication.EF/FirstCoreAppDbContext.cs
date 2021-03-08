using ApiWithbasicAuthentication.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWithbasicAuthentication.EF
{
    public class FirstCoreAppDbContext: DbContext
    {

        public FirstCoreAppDbContext(DbContextOptions<FirstCoreAppDbContext> options) : base(options)
        {
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
