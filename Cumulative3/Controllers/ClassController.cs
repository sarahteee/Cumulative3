using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative3.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Cumulative3.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class/List?ClassSearchKey={value}
        // Go to /Views/Class/List.cshtml
        // Browser opens a list classes page
        public ActionResult List(string ClassSearchKey)
        {
            //check if the search key works
            Debug.WriteLine("I want to search for classes with the key " + ClassSearchKey);

            //pass class information to view
            //create instance of class data controller
            ClassDataController Controller = new ClassDataController();

            List<Class> Classes = Controller.ListClasses(ClassSearchKey);

            //pass class information to views/class/list
            return View(Classes);
        }

        //GET : /Class/Show/{id}
        //Route to /Views/Classes/Show.cshtml
        public ActionResult Show(int id)
        {
            ClassDataController Controller = new ClassDataController();

            Class SelectedClass = Controller.FindClass(id);

            return View(SelectedClass);
        }
    }
}