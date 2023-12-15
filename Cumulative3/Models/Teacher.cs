using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative3.Models
{
    public class Teacher
    {
        //information about a teacher

        //teacher id
        public int TeacherId { get; set; }


        //teacher first name
        public string TeacherFName { get; set;}


        //teacher last name
        public string TeacherLName { get; set;}


        //teacher employee number
        public string EmployeeNumber { get; set;}


        //teacher date of hire
        public DateTime HireDate { get; set; }


        //teacher salary
        public string Salary { get; set; }

    }
}