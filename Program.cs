// See https://aka.ms/new-console-template for more information

using System.Collections;


namespace Student_Project
{

    //Generic Student Method based on Organization
    public class Stud_List
    {
        private static Stud_List instance = null;
        private Stud_List() { }


        private static Dictionary<int, Dictionary<int, College_student>> studList = null;

        public static Stud_List getInstance()
        {
            if (instance == null)
            {
                instance = new Stud_List();
            }
            return instance;
        }

        public void initializeList()
        {
            if (studList == null)
            {
                studList = new Dictionary<int, Dictionary<int, College_student>>();
            }
        }
        public Dictionary<int, Dictionary<int, College_student>> getStudList()
        {
            if (studList == null)
            {
                studList = new Dictionary<int, Dictionary<int, College_student>>();
                studList = DatabaseManager.getStuData(Student_Project_Main.stu_path);
            }
            return studList;
        }
        public void setStudList(Dictionary<int, Dictionary<int, College_student>> list)
        {
            studList = list;
        }

        public Dictionary<int, College_student> getDetails(int id)
        {
            return studList[id];
        }

        public void addDetails(int key, College_student studData)
        {
            if (studList.ContainsKey(key))
            {
                if (studList[key] == null)
                {
                    studList[key] = new Dictionary<int, College_student>();
                    studList[key].Add(studData.reg_no, studData);
                }
                else
                {
                    studList[key].Add(studData.reg_no, studData);
                }
            }
            else
            {
                studList[key] = new Dictionary<int, College_student>();
                studList[key].Add(studData.reg_no, studData);
            }

        }

    }
    //Generic Staff Method based on Organization
    public class Staff_List
    {
        private static Staff_List instance = null;

        private static Dictionary<int, College_staff> staffList = null;

        public Staff_List() { }

        public static Staff_List getInstance()
        {
            if (instance == null)
            {
                instance = new Staff_List();
            }
            return instance;
        }
        public Dictionary<int, College_staff> getStaffList()
        {
            if (staffList == null)
            {
                staffList = new Dictionary<int, College_staff>();
                staffList = DatabaseManager.getStaffData(Student_Project_Main.staff_path);
            }
            return staffList;
        }
        public void setStaffList(Dictionary<int, College_staff> list)
        {
            staffList = list;
        }
        public College_staff getDetails(int id)
        {
            return staffList[id];
        }
        public void addDetails(int id, College_staff staffData)
        {
            staffList.Add(id, staffData);
        }

    }



    //Main Project Method - Starting Point of Project
    public class Student_Project_Main
    {
        public static AppData appInfo;
        public static int authType;
        public static bool login = false;
        public static College_staff staffData = null;
        public static College_student stuData = null;
        public static string stu_path = "F:\\data\\student.txt";
        public static string staff_path = "F:\\data\\staff.txt";

        public static void Main(string[] args)
        {
            SQLDatabaseManager.getInstance().createDatabase();
            SQLDatabaseManager.getInstance().createTable();

            string path = "F:\\data\\appdata.txt";

            if (SQLDatabaseManager.Validation.getInstance().isDataAvailable())
            {
                appInfo = SQLDatabaseManager.GetData.getInstance().getAppdata();
                //SQLDatabaseManager.GetData.getInstance().getStudDataAsync();
            }
            else
            {
                AppData appData = new AppData();
                Console.WriteLine("What is Organization type:\n1.School\n2.College\n3.Exit");
                int ch = int.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    appData.Organization_type = "School";
                }
                else if (ch == 2)
                {
                    appData.Organization_type = "College";
                }
                else
                {
                    throw new Exception("Exited");
                }

                Console.WriteLine("Enter the Organization id");
                appData.Organization_id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the Organization Name");
                appData.Organization_name = Console.ReadLine();
                appInfo = appData;
                SQLDatabaseManager.StoreData.getInstance().storeAppdata(appData);
                //DatabaseManager.storeAppData(path, appData);

            }
            //showOption();
            Console.WriteLine(String.Format("Welcome To Our Organization"));
            Console.WriteLine(String.Format("{0,-5} {1,-15}", " ", appInfo.Organization_name));

            if (appInfo.Organization_type == "School")
            {
                staffData = new College_staff();
                stuData = new College_student();
                showOptions();
            }
            else
            {
                staffData = new College_staff();
                stuData = new College_student();
                showOptions();
            }


        }
        public static void adminOptions()
        {
            Console.WriteLine("Select Option \n1.Update Application Details\n2.Add Departmetns List\n3.Add Hobbies\n4.Add Designations\n5.Exit");
            int ch=int.Parse(Console.ReadLine());
            switch (ch)
            {
                case 1:
                    break;
            }
        }

        //Show the options for login
        public static void showOptions()
        {

            Console.WriteLine("Select option\n1.Staff Login\n2.Student Login\n3.Exit");
            try
            {
                authType = int.Parse(Console.ReadLine());
                if (authType == 1)
                {
                    staffLogin();
                }
                else if (authType == 2)
                {
                    studentLogin();
                }
                else if (authType == 3)
                {
                    Console.WriteLine("Thank You");
                }
                else
                {
                    Console.WriteLine("Please Enter the Correct Option");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please Enter valid input" + e);
            }
        }

        //Verify the staff Login details
        public static void staffLogin()
        {
            Staff_List.getInstance().getStaffList();
            Console.WriteLine("Enter the Staff Id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the passcode");
            string pass = Console.ReadLine();

            //if (Validation.StaffVerification(id, pass) && login)
            if (SQLDatabaseManager.Validation.getInstance().isDataAvailable())
            {
                Stud_List.getInstance().getStudList();
                if (staffData.designation.Equals("Principal"))
                {
                    Console.WriteLine("Enter your Option\n1.Add Staff\n2.Update Salary\n3.Add Student\n4.View Students\n5.Update Student Marks\n6.Update Student Average Mark\n5.Exit");

                }
                while (login)
                {

                    Console.WriteLine("Enter your Option\n1.Add Student\n2.View Students\n3.Update Student Marks\n4.Update Student Average Mark\n5.Exit");
                    int sdOp = int.Parse(Console.ReadLine());
                    switch (sdOp)
                    {
                        case 1:
                            Staff_operations st = new Staff_operation();
                            st.addStudent();
                            break;
                        case 2:
                            Staff_operations st1 = new Staff_operation();
                            st1.viewStudents();
                            break;
                        case 5:
                            login = false;
                            break;
                    }
                }
            }
            else
            {

            }

        }

        //Verify the Student Details
        public static void studentLogin()
        {
            Stud_List.getInstance().getStudList();
            Staff_List.getInstance().getStaffList();
            Console.WriteLine("Enter the Student Id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the passcode");
            string pass = Console.ReadLine();


            if (Validation.StuVerification(id, pass) && login)
            {
                Stud_List.getInstance().getStudList();

                while (login)
                {

                    Console.WriteLine("\nEnter your Option\n1.View Profile\n2.View Marks\n3.Submit Assignment Mark\n4.View Overall Details\n5.Exit");
                    int sdOp = int.Parse(Console.ReadLine());
                    switch (sdOp)
                    {
                        case 1:
                            Student_operations st = new Stud_Operations();
                            st.viewDetails(id, pass);
                            break;
                        case 2:
                            Staff_operations st1 = new Staff_operation();
                            st1.viewStudents();
                            break;
                        case 5:
                            login = false;
                            break;
                    }
                }
            }
            else
            {

            }
        }
    }

}
