using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative3.Models
{
    public class Class
    {
        //information about a class

        //class id
        public int ClassId { get; set; }


        //class code
        public string ClassCode { get; set; }


        //class teacher id
        public int TeacherId { get; set; } 


        //class start date
        public DateTime StartDate { get; set; }


        //class finish date
        public DateTime FinishDate { get; set; }


        //class name
        public string ClassName { get; set; }
    }
}