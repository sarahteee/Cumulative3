using Cumulative3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;

namespace Cumulative3.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This controller accesses the teachers table of the school database.
        /// <summary>
        /// Returns a list of teachers in the database
        /// </summary>
        /// <returns>
        /// List of teacher objects with teacher names containing the search key
        /// </returns>
        /// <param name="TeacherSearchKey">The teachers being searched</param>
        /// <example>
        /// GET api/TeacherData/ListTeachers -> [{"TeacherId":"1", "TeacherFName":"Alexander", "TeacherLName":"Benett", "EmployeeNumber":"T378", "HireDate":"2016-08-05 00:00:00", "Salary":"55.30"
        /// GET api/TeacherData/ListTeachers -> [{"TeacherId":"2", "TeacherFName":"Caitlin", "TeacherLName":"Cummings", "EmployeeNumber":"T381", "HireDate":"2014-06-10 00:00:00", "Salary":"62.77"
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey}")]
        public List<Teacher> ListTeachers(string TeacherSearchKey)
        {
            Debug.WriteLine("trying to do an api search for " + TeacherSearchKey);
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //create command
            MySqlCommand cmd = Conn.CreateCommand();

            //command text SQL query
            cmd.CommandText = "select * from teachers where teacherfname like @key or teacherlname like @key";

            //sanitize the teacher search key input
            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");

            //get a result set for our response
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //set up teacher list
            List<Teacher> Teachers = new List<Teacher>();

            //loop through query results
            while (ResultSet.Read())
            {
                //access column info

                //get teacher id
                int TeacherId = (int)ResultSet["teacherid"];

                //get teacher first name
                string TeacherFName = ResultSet
                ["teacherfname"].ToString();

                //get teacher last name
                string TeacherLName = ResultSet
                ["teacherlname"].ToString();

                //get teacher employee number
                string EmployeeNumber = ResultSet
                ["employeenumber"].ToString();

                //get teacher hire date
                DateTime HireDate = (DateTime)ResultSet
                ["hiredate"];

                //get teacher salary
                string Salary = (ResultSet)
                ["salary"].ToString();

                //create and set info for new teacher object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //add teacher to list
                Teachers.Add(NewTeacher);
            }

            //close connection between server and database
            Conn.Close();

            //return final list of teachers
            return Teachers;
        }

        /// <summary>
        /// Find a teacher by teacherid input
        /// </summary>
        /// <param name="TeacherId">The teacherid primary key in the database</param>
        /// <returns>
        /// Teacher object of the inputted teacherid
        /// </returns>
        /// <example>
        /// GET api/TeacherData/FindTeacher  -> [{"TeacherId":"3", "TeacherFName":"Linda", "TeacherLName":"Chan", "EmployeeNumber":"T382", "HireDate":"2015-08-22 00:00:00", "Salary":"60.22"
        /// GET api/TeacherData/FindTeacher  -> [{"TeacherId":"4", "TeacherFName":"Lauren", "TeacherLName":"Smith", "EmployeeNumber":"T385", "HireDate":"2014-06-22 00:00:00", "Salary":"74.20"
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]

        public Teacher FindTeacher(int TeacherId)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //create command
            MySqlCommand Command = Conn.CreateCommand();

            //set the command text
            Command.CommandText = "select * from teachers where teacherid = " + TeacherId;

            //get result set
            MySqlDataReader ResultSet = Command.ExecuteReader();

            Teacher SelectedTeacher = new Teacher();
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32
                (ResultSet["teacherid"]);

                SelectedTeacher.TeacherFName = ResultSet
                ["teacherfname"].ToString();

                SelectedTeacher.TeacherLName = ResultSet
                ["teacherlname"].ToString();

                SelectedTeacher.EmployeeNumber = ResultSet
                ["employeenumber"].ToString();

                SelectedTeacher.HireDate = (DateTime)ResultSet
                ["hiredate"];

                SelectedTeacher.Salary = (ResultSet)
                ["salary"].ToString();

            }

            //close connection
            Conn.Close();

            return SelectedTeacher;
        }

        /// <summary>
        /// Receives teacher information and adds the information to the database
        /// </summary>
        /// <example>
        /// POST: api/TeacherData/AddTeacher
        /// 
        /// FORM DATA / POST DATA:
        /// {
        ///     "TeacherFName":"Michael",
        ///     "TeacherLName":"Meyers",
        ///     "EmployeeNumber":"T123"
        ///     "HireDate": "2023-10-31 00:00:00"
        ///     "Salary":"55.13"
        /// }
        /// </example>
        /// <example>
        /// curl -d "{\"TeacherId\":\"6\", \"TeacherFName\":\"Dwight\", \"TeacherLName\":\"Schrute\", \"EmployeeNumber\":\"T003\", \"HireDate\":\"2014-06-22 00:00:00\", \"Salary\":\"87.20\"}" -H "Content-Type: application/json"
        /// http://localhost:57230/api/teacherdata/addteacher
        /// </example>
        /// <returns>
        /// </returns>

        //new method in teacherdata controller
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //INSERT INTO teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES(0, 'Michael', 'Meyers', 'T123', '2023-10-31 00:00:00', 55.13);

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand CMD = Conn.CreateCommand();

            string query = "INSERT INTO teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES(0,@teacherfname,@teacherlname,@employeenumber,@hiredate,@salary)";

            CMD.CommandText = query;
            CMD.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            CMD.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            CMD.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            CMD.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            CMD.Parameters.AddWithValue("@salary", NewTeacher.Salary);

            CMD.Prepare();

            CMD.ExecuteNonQuery();

            Conn.Close();

        }

        /// <summary>
        /// Delete a teacher in the system
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="TeacherId">The teacher ID in the system</param>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher/1 ->
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand CMD = Conn.CreateCommand();

            string query = "delete from teachers where teacherid=@teacherid";
            CMD.CommandText = query;
            CMD.Parameters.AddWithValue("@teacherid", TeacherId);

            CMD.Prepare();
            CMD.ExecuteNonQuery();
            Conn.Close();

        }

        ///<summary>
        ///Updates the teacher info in the database
        ///</summary>
        ///<param name="TeacherId">The teacherid to update</param>
        ///<param name="UpdatedTeacher">teacher object containing the new info</param>
        ///<example>
        /// POST api/TeacherData/UpdateTeacher/1
        /// {
        ///     "TeacherFName": "Alex",
        ///     "TeacherLName": "Smith",
        ///     "Salary": "60.55"
        /// }
        ///</example>
        ///<return></return>

        [HttpPost]
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        public void UpdateTeacher(int TeacherId, Teacher UpdatedTeacher)
        {
            Debug.WriteLine("The API has been reached!");
            Debug.WriteLine("The ID is " +TeacherId);
            Debug.WriteLine("The updated first name is " +UpdatedTeacher.TeacherFName);
            Debug.WriteLine("The updated last name is" +UpdatedTeacher.TeacherLName);
            Debug.WriteLine("The updated salary is" +UpdatedTeacher.Salary);

            //build the update logic
            //query

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();
            MySqlCommand Cmd = Conn.CreateCommand();

            string query = "update teachers set teacherfname=@TeacherFName, teacherlname=@TeacherLName, salary=@Salary where teacherid=@TeacherId";

            Cmd.CommandText = query;
            Cmd.Parameters.AddWithValue("@TeacherFName", UpdatedTeacher.TeacherFName);
            Cmd.Parameters.AddWithValue("@TeacherLName", UpdatedTeacher.TeacherLName);
            Cmd.Parameters.AddWithValue("@Salary", UpdatedTeacher.Salary);
            Cmd.Parameters.AddWithValue("@TeacherId", TeacherId);

            Cmd.Prepare();

            Cmd.ExecuteNonQuery();

            Conn.Close();

            return;
        }
    }
}
