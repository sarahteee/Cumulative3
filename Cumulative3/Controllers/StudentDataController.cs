using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Cumulative3.Models;

namespace Cumulative3.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This controller accesses the students table of the school database.
        /// <summary>
        /// Returns a list of students in the database
        /// </summary>
        /// <returns>
        /// List of student objects with student names containing the search key
        /// </returns>
        /// <param name="StudentSearchKey">The student being searched</param>
        /// <example>
        /// GET api/StudentData/ListStudents -> [{"StudentId":"1", "StudentFName":"Sarah", "StudentLName":"Valdez", "StudentNumber":"N1678", "EnrolDate":"2018-06-18"
        /// GET api/StudentData/ListStudents -> [{"StudentId":"2", "StudentFName":"Jennifer", "StudentLName":"Faulkner", "StudentNumber":"N1679", "EnrolDate":"2018-08-02"
        /// </example>

        [HttpGet]
        [Route("api/StudentData/ListStudents/{StudentSearchKey}")]
        public List<Student> ListStudents(string StudentSearchKey)
        {
            Debug.WriteLine("trying to do an api search for " + StudentSearchKey);

            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //create command
            MySqlCommand cmd = Conn.CreateCommand();

            //command text SQL query
            cmd.CommandText = "select * from students where studentfname like @key or studentlname like @key or studentid like @key";

            //sanitize the student search key input
            cmd.Parameters.AddWithValue("@key", "%" + StudentSearchKey + "%");

            //get a result set for our response
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //set up student list
            List<Student> Students = new List<Student>();

            //loop through query results
            while (ResultSet.Read())
            {
                //access column info

                //get student id
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);

                //get student first name
                string StudentFName = ResultSet
                ["studentfname"].ToString();

                //get student last name
                string StudentLName = ResultSet
                ["studentlname"].ToString();

                //get student number
                string StudentNumber = ResultSet
                ["studentnumber"].ToString();

                //get student enrollment date
                DateTime EnrolDate = (DateTime)ResultSet
                ["enroldate"];

                //create and set info for new student object
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFName = StudentFName;
                NewStudent.StudentLName = StudentLName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

                //add student to list
                Students.Add(NewStudent);
            }

            //close connection between server and database
            Conn.Close();

            //return final list of students
            return Students;
        }

        /// <summary>
        /// Find a student by studentid input
        /// </summary>
        /// <param name="StudentId">The studentid primary key in the database</param>
        /// <returns>
        /// Student object of the inputted studentid
        /// </returns>
        /// <example>
        /// GET api/StudentData/FindStudent  -> [{"StudentId":"3", "StudentFName":"Austin", "StudentLName":"Simon", "StudentNumber":"N1682", "EnrolDate":"2018-06-14"
        /// GET api/StudentData/FindStudent  -> [{"StudentId":"4", "StudentFName":"Mario", "StudentLName":"English", "StudentNumber":"N1686", "EnrolDate":"2018-07-03"
        /// </example>

        [HttpGet]
        [Route("api/StudentData/FindStudent/{StudentId}")]

        public Student FindStudent(int StudentId)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //create command
            MySqlCommand Command = Conn.CreateCommand();

            //set the command text
            Command.CommandText = "select * from students where studentid = " + StudentId;

            //get result set
            MySqlDataReader ResultSet = Command.ExecuteReader();

            Student SelectedStudent = new Student();
            while (ResultSet.Read())
            {
                SelectedStudent.StudentId = Convert.ToInt32
                (ResultSet["studentid"]);

                SelectedStudent.StudentFName = ResultSet
                ["studentfname"].ToString();

                SelectedStudent.StudentLName = ResultSet
                ["studentlname"].ToString();

                SelectedStudent.StudentNumber = ResultSet
                ["studentnumber"].ToString();

                SelectedStudent.EnrolDate = (DateTime)ResultSet
                ["enroldate"];

            }

            //close connection
            Conn.Close();

            return SelectedStudent;
        }
    }
}