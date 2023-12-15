using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative3.Models
{
    public class Student
    {
        //information about a student

        //student id
        public int StudentId { get; set; }


        //student first name
        public string StudentFName { get; set; }


        //student last name
        public string StudentLName { get; set; }


        //student number
        public string StudentNumber { get; set; }


        //date of enrollment
        public DateTime EnrolDate { get; set; }
    }
}