// See https://aka.ms/new-console-template for more information

using System.Collections;
namespace Student_Project
{

    //Generic Student Method based on Organization
    public class Stud_List
    {
        private static Stud_List instance = null;
        private Stud_List() { }


        private static Dictionary<Data_model.Standard, List<School_student>> studList = null;

        public static Stud_List getInstance()
        {
            if(instance == null)
            {
                instance = new Stud_List();
            }
            return instance;
        }

        public void initializeList()
        {
            if(studList == null)
            {
                studList = new Dictionary<Data_model.Standard, List<School_student>>();
            }
        }
        public Dictionary<Data_model.Standard, List<School_student>> getStudList()
        {
            if(studList == null)
            {
                studList = new Dictionary<Data_model.Standard, List<School_student>>();
                studList=DatabaseManager.getStuData(Student_Project_Main.stu_path);
            }
            return studList;
        }
        public void setStudList(Dictionary<Data_model.Standard, List<School_student>> list)
        {
            studList = list;
        }

        public List<School_student> getDetails(Data_model.Standard id)
        {
            return studList[id];
        }

        public void addDetails(Data_model.Standard sd,School_student studData)
        {
            if (studList.ContainsKey(sd))
            {
                if(studList[sd] == null)
                {
                    studList[sd]=new List<School_student>();
                    studList[sd].Add(studData);
                }
                else
                {
                    studList[sd].Add(studData);
                }
            }
            else
            {
                var stList= new List<School_student>();
                stList.Add(studData);
                studList.Add(sd,stList);
                
            }
            
        }
    }

    //Generic Staff Method based on Organization
    public class Staff_List
    {
        private static Staff_List instance = null;

        private static Dictionary<int, School_staff> staffList = null;

        public Staff_List() { }

        public static Staff_List getInstance()
        {
            if (instance == null)
            {
                instance=new Staff_List();   
            }
            return instance;
        }
        public Dictionary<int, School_staff> getStaffList()
        {
            if (staffList == null)
            {
               staffList = new Dictionary<int, School_staff>();
               staffList = DatabaseManager.getStaffData(Student_Project_Main.staff_path);
            }
            return staffList;
        }
        public void setStaffList(Dictionary<int, School_staff> list)
        {
            staffList=list;
        }
        public School_staff getDetails(int id)
        {
            return staffList[id];
        }
        public void addDetails(int id, School_staff staffData)
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
        public static Staff staffData = null;
        public static Student stuData = null;
        public static string stu_path = "F:\\data\\student.txt";
        public static string staff_path = "F:\\data\\staff.txt";

        public static void Main(string[] args)
        {
            var a = new string[] { "Hello" };
            Console.WriteLine(a.Length);
            string path = "F:\\data\\appdata.txt";
            //Student_Management.DatabaseManager.getAppData();
            if (Validation.isDataAvailable(path))
            {
                appInfo = DatabaseManager.getAppData(path);
            }
            else
            {
                AppData appData = new AppData();
                Console.WriteLine("What is Organization type:\n1.School\n2.College\n3.Exit");
                int ch = int.Parse(Console.ReadLine());
                if (ch == 1)
                {
                    appData.org_type = "School";
                }
                else if(ch==2)
                {
                    appData.org_type = "College";
                }
                else
                {
                    throw new Exception("Exited");
                }
                
                Console.WriteLine("Enter the Organization id");
                appData.org_id=int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the Organization Name");
                appData.org_name=Console.ReadLine();
                appInfo = appData;
                DatabaseManager.storeAppData(path, appData);

            }
            //showOption();
            Console.WriteLine(String.Format("Welcome To Our Organization"));
            Console.WriteLine(String.Format("{0,-5} {1,-15}", " ",appInfo.org_name));
            
            if (appInfo.org_type == "School")
            {
                staffData=new School_staff();
                stuData = new School_student();
                showOptions();
            }
            else
            {
                staffData = new College_staff();
                stuData = new College_student();
            }
            

        }

        //Show the options for login
        public static void showOptions()
        {
            
            Console.WriteLine("Select option\n1.Staff Login\n2.Student Login\n3.Exit");
            try
            {
                authType = int.Parse(Console.ReadLine());
                if(authType == 1)
                {
                    staffLogin();
                }
                else if(authType == 2)
                {
                    studentLogin();
                }
                else
                {
                    
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
            int id=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the passcode");
            string pass=Console.ReadLine();
            
            if (Validation.StaffVerification(id, pass))
            {
                Stud_List.getInstance().getStudList();
                
                while (true)
                {

                    Console.WriteLine("Enter your Option\n1.Add Student\n2.View Students\n3.Update Student Marks\n4.Update Student Average Mark\n5.Exit");
                    int sdOp=int.Parse(Console.ReadLine());
                    switch (sdOp)
                    {
                        case 1:
                            Staff_operations st = new Staff_operation();
                            st.addStudent();
                            break;
                        case 2:
                            Staff_operations st1 = new Staff_operation();
                            st1.viewStudents();
                            break ;
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
            Validation.StaffVerification(id, pass);
        }
    }
}
