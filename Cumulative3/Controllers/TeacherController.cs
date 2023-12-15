using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative3.Models;
using System.Diagnostics;

namespace Cumulative3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List?TeacherSearchKey={value}
        // Go to /Views/Teacher/List.cshtml
        // Browser opens a list teachers page
        public ActionResult List(string TeacherSearchKey)
        {
            //Check if the search key works
            Debug.WriteLine("I want to search for teachers with the key " + TeacherSearchKey);

            //pass teacher information to view
            //create instance of teacher data controller
            TeacherDataController Controller = new TeacherDataController();

            List<Teacher> Teachers = Controller.ListTeachers(TeacherSearchKey);

            //pass teacher information to /views/teacher/list
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        //Route to /Views/Teachers/Show.cshtml
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //GET: /Teacher/New
        //Route to /Views/Teacher/New.cshtml

        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(Teacher NewTeacher)
        {
            //Get the submitted teacher info
            Debug.WriteLine("The following teacher information has been received: " + NewTeacher.TeacherFName);

            //add the submitted teacher info to the database
            TeacherDataController Controller = new TeacherDataController();

            Controller.AddTeacher(NewTeacher);



            //return to original teacher list
            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteConfirm/{teacherid}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            Teacher SelectedTeacher = Controller.FindTeacher(id);


            return View(SelectedTeacher);
        }

        //POST: /Teacher/Delete/{teacherid}
        [HttpPost]

        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }


        /// <summary>
        /// routes to a dynamically generated "edit teacher info" page
        /// </summary>
        /// <param name="id">id of the teacher</param>
        /// <returns> a dynamic "edit teacher info" page which provides current teacher info and presents a form for users to edit the info</returns>
        /// <example>GET: /Teacher/Edit/1</example>
        public ActionResult Edit(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            //display teacher information
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

 
        /// <summary>
        /// receives a post request containing information about an existing teacher, with new values. sends this info to the api and redirects to show page of updated teacher
        /// </summary>
        /// <param name="id"> id of the teacher to update</param>
        /// <param name="UpdatedTeacher">teachers updated information</param>
        /// <returns>a dynamic webpage that gives current teacher info</returns>
        /// <example>
        /// POST : /Teacher/Update/1
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "TeacherFName":"Albus",
        ///     "TeacherLName":"Dumbledore",
        ///     "Salary":"99.99"
        ///  }    
        /// </example>

        [HttpPost]

        public ActionResult Update(int id, Teacher UpdatedTeacher)
        {
            //confirm we recieve the information
            //Debug.WriteLine("the teacher id is "+id);
            //Debug.WriteLine("the teachers first name is " + UpdatedTeacher.TeacherFName);
            //Debug.WriteLine("the teachers last name is " + UpdatedTeacher.TeacherLName);
            //Debug.WriteLine("the teachers salary is " + UpdatedTeacher.Salary);


            TeacherDataController Controller = new TeacherDataController();

            Controller.UpdateTeacher(id, UpdatedTeacher);

            //return to the show teacher page
            return RedirectToAction("Show/" + id);
        }
    }
}