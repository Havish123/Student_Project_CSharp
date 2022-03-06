using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Z.Dapper.Plus;

namespace Student_Project
{
    public class SQLDatabaseManager
    {



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
            //Console.WriteLine("Database Created");
            string connStringDB = "Server=UNVPNSI456L2824;User Id=sa;Password=10107@Havish;";
            SQLDatabase sql = new SQLDatabase();
            conn = new SqlConnection(connStringDB);
            conn.Open();
            var data = conn.Query($"IF NOT EXISTS(SELECT * FROM SYS.DATABASES WHERE NAME='{sql.DatabaseName}') BEGIN CREATE DATABASE {sql.DatabaseName} END;");
            conn.Close();
        }

        public async void createTable()
        {
            //Console.WriteLine("Table Created");
            var sql = new SQLDatabase.Student_table();
            var dep = new SQLDatabase.departments();
            var des = new SQLDatabase.designation();
            var hob = new SQLDatabase.Hobbies();
            var staff = new SQLDatabase.Staff_table();
            var app = new SQLDatabase.App_table();
            try
            {
                var stud_table = await conn.ExecuteAsync(
                $"USE {new SQLDatabase().DatabaseName} IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME='{app.table}') BEGIN CREATE TABLE {app.table} ({app.org_id} INT PRIMARY KEY NOT NULL,{app.org_name} VARCHAR(50) NOT NULL,{app.org_type} VARCHAR(20) NOT NULL) END " +
                $"IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE NAME='{dep.table}' ) BEGIN CREATE TABLE {dep.table}({dep.id} INT PRIMARY KEY NOT NULL,{dep.name} VARCHAR(30) NOT NULL) END " +
                $"IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE NAME='{des.table}' ) BEGIN CREATE TABLE {des.table}( {des.id} INT PRIMARY KEY NOT NULL,{des.name} VARCHAR(30) NOT NULL,{des.salary} INT NOT NULL) END " +
                $"IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE NAME='{hob.table}' ) BEGIN CREATE TABLE {hob.table} ({hob.id} INT PRIMARY KEY NOT NULL, {hob.name} VARCHAR(30) NOT NULL) END " +
                $"IF NOT EXISTS (SELECT * FROM SYS.TABLES WHERE NAME='{staff.table}') BEGIN CREATE TABLE {staff.table}({staff.id} INT PRIMARY KEY NOT NULL,{staff.name} VARCHAR(20) NOT NULL,{staff.email} VARCHAR(30) NOT NULL,{staff.passcode} VARCHAR(20) NOT NULL,{staff.phone_no} VARCHAR(10) NOT NULL,{des.id} INT FOREIGN KEY REFERENCES {des.table}({des.id}),{dep.id} INT FOREIGN KEY REFERENCES {dep.table}({dep.id})) END " +
                $"IF NOT EXISTS ( SELECT * FROM SYS.TABLES WHERE NAME='{sql.Table_Name}' ) BEGIN CREATE TABLE {sql.Table_Name}({sql.reg_no} INT NOT NULL PRIMARY KEY,{sql.f_name} VARCHAR(20),{sql.l_name} VARCHAR(20)," +
                $"{sql.dob} DATE,{sql.description} VARCHAR(30),{sql.joining_date} DATE,{sql.hob_id} INT FOREIGN KEY REFERENCES {hob.table}({hob.id}),{sql.passcode} VARCHAR(10) NOT NULL,{sql.dep_id} int FOREIGN KEY REFERENCES {dep.table}({dep.id}),{sql.email_id} VARCHAR(30)," +
                $"{sql.phone_no} VARCHAR(10) NOT NULL,{sql.sslc_mark} FLOAT,{sql.hsc_mark} FLOAT,{sql.cgpa} FLOAT) END");
                //Console.WriteLine("Table Created");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            //Console.WriteLine("Table Created");
            conn.Close();
        }


        public void openCon()
        {
            string connStringSQL = $"Server=UNVPNSI456L2824;Database={new SQLDatabase().DatabaseName};User Id=sa;Password=10107@Havish;";
            conn = new SqlConnection(connStringSQL);
            conn.Open();
        }

        public void closeCon()
        {
            conn.Close();
        }

        public class GetData
        {
            public static GetData instance = null;
            public SQLDatabaseManager sqlmngr = SQLDatabaseManager.getInstance();
            public static GetData getInstance()
            {
                if (instance == null)
                {
                    instance = new GetData();
                }
                return instance;
            }

            public AppData getAppdata()
            {
                //Console.WriteLine("Hi");
                var apptable = new SQLDatabase.App_table();
                sqlmngr.openCon();
                var conn = SQLDatabaseManager.getInstance().conn;
                var appdata = conn.QueryFirst<AppData>($"SELECT * FROM {apptable.table};");
                //Console.WriteLine(appdata.Organization_name+"hi");
                sqlmngr.closeCon();
                return appdata;
            }

            public List<Departments> getDepartmentData()
            {
                var depTable = new SQLDatabase.departments();
                sqlmngr.openCon();
                var conn = SQLDatabaseManager.getInstance().conn;
                List<Departments> depList = conn.Query<Departments>($"SELECT * FROM {depTable.table};").ToList<Departments>();
                sqlmngr.closeCon();
                return depList;
            }

            public List<Hobbies> getHobbies()
            {
                var hobtable = new SQLDatabase.Hobbies();
                sqlmngr.openCon();
                var conn = SQLDatabaseManager.getInstance().conn;
                List<Hobbies> hobList = conn.Query<Departments>($"SELECT * FROM {hobtable.table};").ToList<Hobbies>();
                sqlmngr.closeCon();
                return hobList;
            }

            public async void getStudDataAsync()
            {

                var studTable = new SQLDatabase.Student_table();
                var depTable = new SQLDatabase.departments();
                var hobTable = new SQLDatabase.Hobbies();
                sqlmngr.openCon();
                var conn = SQLDatabaseManager.getInstance().conn;
                string sql = $"SELECT * FROM {studTable.Table_Name} s INNER JOIN {hobTable.table} h ON s.{studTable.hob_id}=h.{hobTable.id} INNER JOIN {depTable.table} d ON s.{studTable.dep_id}=d.{depTable.id};";
                Console.WriteLine(sql);
                var studdata = await conn.QueryAsync<College_student, Hobbies, Departments, Student>(sql, (student, hobbies, departments) =>
                       {
                           student.departments = departments;
                           student.hobbies = hobbies;
                           return student;
                       },
                    splitOn: "hob_id,dep_id"
                    );
                var stList = studdata.ToList();
                foreach (var st in stList)
                {
                    Console.WriteLine(st.f_name + " " + st.hobbies.hob_name);
                }
                sqlmngr.closeCon();
                //return studdata;
            }
        }

        public class StoreData
        {
            public static StoreData instance = null;

            public SQLDatabaseManager sqlmngr = SQLDatabaseManager.getInstance();
            public static StoreData getInstance()
            {
                if (instance == null)
                {
                    instance = new StoreData();
                }
                return instance;
            }

            //Store the Application Data
            public AppData storeAppdata(AppData appdata)
            {
                try
                {
                    var apptable = new SQLDatabase.App_table();
                    sqlmngr.openCon();
                    var conn = SQLDatabaseManager.getInstance().conn;
                    var sql = $"INSERT INTO {apptable.table}({apptable.org_id},{apptable.org_name},{apptable.org_type}) VALUES({appdata.Organization_id},'{appdata.Organization_name}','{appdata.Organization_type}');";
                    Console.WriteLine(sql);
                    var count = conn.ExecuteScalar<AppData>(sql);
                    Console.WriteLine("Successfully Configured Application Data.....");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not Configured your application....Please Contact Admin....");
                }
                finally
                {
                    sqlmngr.closeCon();
                }
                return appdata;

            }

            //Store the Departments Data
            public void storeDepData(List<Departments> depList)
            {
                try
                {
                    var dep = new SQLDatabase.departments();
                    sqlmngr.openCon();
                    var conn = SQLDatabaseManager.getInstance().conn;
                    conn.BulkInsert(depList);
                    Console.WriteLine("Successfully Inserted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not Insert Departments data....please try again...");
                }
                finally
                {
                    sqlmngr.closeCon();
                }
               
            }

            //Store the Hobbies Data
            public void storeHobData(List<Hobbies> hobList)
            {
                try
                {
                    var hob = new SQLDatabase.Hobbies();
                    sqlmngr.openCon();
                    var conn = SQLDatabaseManager.getInstance().conn;
                    conn.BulkInsert(hobList);
                    Console.WriteLine("Successfully Inserted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not Insert Hobbies data....please try again...");
                }
                finally
                {
                    sqlmngr.closeCon();
                }
               
            }

            //Store the Student List
            public void storeStudData(College_student studData) 
            {

            }

            //Store the Staff Data
            public void storeStaffData(College_staff staffData)
            {

            }

        }

        public class Validation
        {
            public static Validation instance = null;

            public static Validation getInstance()
            {
                if (instance == null)
                {
                    instance = new Validation();
                }
                return (Validation)instance;
            }
            public bool isDataAvailable()
            {
                var app = new SQLDatabase.App_table();
                try
                {
                    SQLDatabaseManager.getInstance().openCon();
                    var conn = SQLDatabaseManager.getInstance().conn;
                    var count = conn.ExecuteScalar($"SELECT COUNT(*) FROM {app.table};");
                    SQLDatabaseManager.getInstance().closeCon();
                    if (int.Parse(count.ToString()) > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                return false;
            }
            public bool staffVerification(int id, string pass)
            {
                var app = new SQLDatabase.Staff_table();
                var conn = SQLDatabaseManager.getInstance().conn;
                var count = conn.ExecuteScalar($"SELECT COUNT(*) FROM {app.table} WHERE {app.id}=={id} and {app.passcode};");
                SQLDatabaseManager.getInstance().closeCon();
                return false;
            }
        }


    }
    public class SQLDatabase
    {
        public string DatabaseName = "School";

        public class App_table
        {
            public string table = "Application";
            public string org_id = "Organization_id";
            public string org_name = "Organization_name";
            public string org_type = "Organization_type";
        }
        public class Student_table
        {
            public string Table_Name = "Students";
            public string reg_no = "reg_no";
            public string f_name = "f_name";
            public string l_name = "l_name";
            public string dob = "dob";
            public string email_id = "email";
            public string phone_no = "phone_no";
            public string sslc_mark = "SSLC_Mark";
            public string hsc_mark = "HSC_mark";
            public string cgpa = "cgpa";
            public string description = "description";
            public string joining_date = "joining_date";
            public string passcode = "passcode";
            public string dep_id = new departments().id;
            public string hob_id = new Hobbies().id;
        }

        public class Staff_table
        {
            public string table = "Staff";
            public string id = "staff_id";
            public string name = "staff_name";
            public string email = "staff_email";
            public string phone_no = "phone_no";
            public string salary = "salary";
            public string passcode = "passcode";
            public string des_id = new designation().id;
            public string dep_id = new departments().id;

        }

        public class departments
        {
            public string table = "Departments";
            public string id = "dep_id";
            public string name = "dep_name";
        }

        public class designation
        {
            public string table = "Designation";
            public string id = "des_id";
            public string name = "des_name";
            public string salary = "des_salary";
        }

        public class Hobbies
        {
            public string table = "Hobbies";
            public string id = "hob_id";
            public string name = "hob_name";

        }

    }
}
