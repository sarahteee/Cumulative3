﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Cumulative3.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get
            {
                return "server =" + Server
                + "; user =" + User
                + "; database =" + Database
                + "; port =" + Port
                + "; password =" + Password
                + "; convert zero datetime = True";

            }
        }
        ///<summary>
        ///Returns a connection to the school database
        ///</summary>
        ///<example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        ///</example>
        ///<returns></returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}