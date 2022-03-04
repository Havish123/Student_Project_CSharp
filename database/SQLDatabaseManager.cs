using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Student_Project
{
    public class SQLDatabaseManager
    {
        public SQLDatabase sql = new SQLDatabase();
        public static string connStringDB = "Server=DESKTOP-MO01FN4;User Id=sa;Password=10107@Havish;";
        public static string connStringSQL = $"Server=DESKTOP-MO01FN4;Database={new SQLDatabase().DatabaseName};User Id=sa;Password=10107@Havish;";
        public static SQLDatabaseManager instance = null;
        public SqlConnection conn;

        private SQLDatabaseManager()
        {

        }

        public static SQLDatabaseManager getInstance()
        {
            if (instance == null)
            {
                instance = new SQLDatabaseManager();
            }
            return instance;
        }

        public void createDatabase()
        {
            conn = new SqlConnection(connStringDB);
            conn.Open();
            var data = conn.Query($"CREATE DATABASE {sql.DatabaseName}");
            conn.Close();
        }

        public void createTable()
        {
            conn = new SqlConnection(connStringSQL);
            conn.Open();
            var data = conn.Execute($"CREATE TABLE {sql.Table_Name}({sql.reg_no} INT NOT NULL PRIMARY KEY,{sql.f_name} VARCHAR(20),{sql.l_name} VARCHAR(20),{sql.dob} DATE,{sql.description} VARCHAR(30),{sql.joining_date} DATE,{sql.average_mark} INT,{sql.standard} VARCHAR(10),{sql.section} VARCHAR(10));");
            conn.Close();
        }

        public void InsertData()
        {

        }

        public void getData()
        {

        }
    }
    public class SQLDatabase
    {
        public string DatabaseName = "School";


        public class Student_table
        {
            public string Table_Name = "Students";
            public string reg_no = "reg_no";
            public string f_name = "f_name";
            public string l_name = "l_name";
            public string dob = "dob";
            public string description = "description";
            public string joining_date = "joining_date";
            public string average_mark = "avg_mark";
            public string standard = "standard";
            public string section = "section";
        }

        public class Staff_table
        {

        }

        public class

    }
}
