using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative3.Models;
using System.Diagnostics;

namespace Cumulative3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student/List?StudentSearchKey={value}
        // Go to /Views/Student/List.cshtml
        // Browser opens a list students page
        public ActionResult List(string StudentSearchKey)
        {
            //Check if the search key works
            Debug.WriteLine("I want to search for students with the key " + StudentSearchKey);

            //pass student information to view
            //create instance of student data controller
            StudentDataController Controller = new StudentDataController();

            List<Student> Students = Controller.ListStudents(StudentSearchKey);

            //pass student information to /views/student/list
            return View(Students);
        }

        //GET : /Student/Show/{id}
        //Route to /Views/Students/Show.cshtml
        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();

            Student SelectedStudent = Controller.FindStudent(id);

            return View(SelectedStudent);
        }

    }
}