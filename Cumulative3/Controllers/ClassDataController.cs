using Cumulative3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Security.Claims;

namespace Cumulative3.Controllers
{
    public class ClassDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        ///This controller accesses the class table of the school database
        ///<summary>
        /// returns a list of classes in the database
        ///</summary>
        ///<returns>
        /// list of class objects with class names containing the search key
        ///</returns>
        ///<param name="ClassSearchKey">The class being searched</param>
        ///<example>
        /// GET api/ClassData/ListClasses -> [{"ClassId":"1", "ClassCode":"http501", "TeacherId":"1", "StartDate":"2018-09-04", "FinishDate":"2018-12-14", "ClassName":"Web Application Development"
        /// GET api/ClassData/ListClasses -> [{"ClassId":"2", "ClassCode":"http502", "TeacherId":"2", "StartDate":"2018-09-04", "FinishDate":"2018-12-14", "ClassName":"Project Managment"
        /// </example>

        [HttpGet]
        [Route("api/ClassData/ListClasses/{ClassSearchKey}")]
        public List<Class> ListClasses(string ClassSearchKey)
        {
            Debug.WriteLine("trying to do an api search for" + ClassSearchKey);

            //create connection
            MySqlConnection Conn = School.AccessDatabase();


            //open connection
            Conn.Open();


            //create command
            MySqlCommand cmd = Conn.CreateCommand();


            //command text SQL query
            cmd.CommandText = "select * from Classes where classname like @key or classcode like @key";


            //sanitize the class search key input
            cmd.Parameters.AddWithValue("@key", "%" + ClassSearchKey + "%");


            //get a result set for our response
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            //set up class list
            List<Class> Classes = new List<Class>();


            //loop through query results 
            while (ResultSet.Read())
            {
                //access column info

                //get class id 
                int ClassId = (int)ResultSet["classid"];

                //get class code
                string ClassCode = ResultSet
                ["classcode"].ToString();

                //get teacher id
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);


                //get class start date 
                DateTime StartDate = (DateTime)ResultSet["startdate"];


                //get class finish date
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];


                //get class name 
                string ClassName = (ResultSet)["classname"].ToString();


                //create and set info for new class object
                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.TeacherId = TeacherId;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;

                //add class to list
                Classes.Add(NewClass);
            }

            //close connection between server and database
            Conn.Close();

            //return final list of classes
            return Classes;
        }

        /// <summary>
        /// Find a class by class id input
        /// </summary>
        /// <param name="ClassId">The classid primary key in the database</param>
        /// <returns>
        /// Class object of the inputted classid
        /// </returns>
        /// <example>
        /// GET api/ClassData/FindClass  -> [{"ClassId":"3", "ClassCode":"http5103", "TeacherId":"5", "StartDate":"2018-09-04", "FinishDate":"2018-12-14", "ClassName":"Web Programming"
        /// GET api/ClassData/FindClass  -> [{"ClassId":"4", "ClassCode":"http5104", "TeacherId":"7", "StartDate":"2018-09-04", "FinishDate":"2018-12-14", "ClassName":"Digital Design"
        /// </example>

        [HttpGet]
        [Route("api/ClassData/FindClass/{ClassId}")]

        public Class FindClass(int ClassId)
        {
            //create connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection
            Conn.Open();

            //create command
            MySqlCommand Command = Conn.CreateCommand();

            //set the command text
            Command.CommandText = "select * from classes where classid = " + ClassId;

            //get result set
            MySqlDataReader ResultSet = Command.ExecuteReader();

            Class SelectedClass = new Class();
            while (ResultSet.Read())
            {
                SelectedClass.ClassId = Convert.ToInt32
                (ResultSet["classid"]);

                SelectedClass.ClassCode = ResultSet
                ["classcode"].ToString();

                SelectedClass.TeacherId = Convert.ToInt32
                (ResultSet["teacherid"]);

                SelectedClass.StartDate = (DateTime)ResultSet
                ["startdate"];

                SelectedClass.FinishDate = (DateTime)ResultSet
                ["finishdate"];

                SelectedClass.ClassName = (ResultSet)
                ["classname"].ToString();

            }

            //close connection
            Conn.Close();

            return SelectedClass;
        }
    }
}