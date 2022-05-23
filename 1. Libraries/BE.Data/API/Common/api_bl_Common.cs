using BE.Services.DbConnections;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Data.API.Common
{
    public class api_bl_Common
    {
        public void ProcessCommonData()
        {
            //    using (var _dbContext = new SqlDbContext())
            //    {
            //        var idParam = new SqlParameter
            //        {
            //            ParameterName = "StudentID",
            //            Value = 1
            //        };
            //        //Get student name of string type
            //        //var courseList = dbContext.Database.SqlQuery<Course>("exec GetCoursesByStudentId @StudentId ", idParam).ToList<Course>();
            //        _dbContext.ExecuteSqlCommand("EXEC spCal_PrdPromotion @Param", false, null, pParamJson);
            //        _dbContext.SaveChanges();

            //        //Or can call SP by following way
            //        //var courseList = ctx.Courses.SqlQuery("exec GetCoursesByStudentId @StudentId ", idParam).ToList<Course>();

            //        foreach (Course cs in courseList)
            //            Console.WriteLine("Course Name: {0}", cs.CourseName);
            //    }
        }
    }
}
