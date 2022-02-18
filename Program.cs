// See https://aka.ms/new-console-template for more information
namespace Student_Management
{

    //Serializable Model for Application Details
    [Serializable]
    public class AppData
    {
        public int org_id { get; set; }
        public string org_type { get; set; }
        public string org_name { get; set; }

    }

    //Generic Student Method based on Organization
    public class Stud_List<T>
    {
        private static Stud_List<T> instance = null;
        private Stud_List() { }
        private static Dictionary<int, T> studList = null;

        public static Stud_List<T> getInstance()
        {
            if(instance == null)
            {
                instance = new Stud_List<T>();
            }
            return instance;
        }

        public Dictionary<int, T> getStudList()
        {
            if(studList == null)
            {
                studList=DatabaseManager.getStuData<T>(new Student_Project().stu_path);
            }
            return studList;
        }
        public void setStudList(Dictionary<int, T> list)
        {
            studList = list;
        }

        public T getDetails(int id)
        {
            return studList[id];
        }

        public void addDetails(int id,T studData)
        {
            studList.Add(id,studData);
        }
    }

    //Generic Staff Method based on Organization
    public class Staff_List<T>
    {
        private static Dictionary<int, T> staffList { get; set; }

        public Dictionary<int, T> getStaffList()
        {
            return staffList;
        }
        public void setStaffList(Dictionary<int, T> list)
        {
            staffList=list;
        }
        public T getDetails(int id)
        {
            return staffList[id];
        }
        public void addDetails(int id, T staffData)
        {
            staffList.Add(id, staffData);
        }

    }

    //Data Model for provide Constant Data or Provide Data for the Project
    public class Data_model
    {
        public static Data_model instance = null;

        //Singleton Method
        public static Data_model getInstance()
        {
            if (instance == null)
                instance = new Data_model();
            return instance;
        }

        //private Constructor
        private Data_model()
        {

        }

        //Departments List if application use in College
        Dictionary<string, string> departments = new Dictionary<string, string>()
        {
            {"MECH","Mechanical Engineering" },
            {"CSE","Computer Science Engineering" },
            {"CIVIL","Civil Engineering" },
            {"EEE","Electrical and Electronics Engineering" },
            {"ECE","Electronics and Communication Engineering" },
            {"AE","Aeronautical Engineering" }
        };
        
        //Standart List if application use in Schools
        public enum Standard
        {
            First=1,
            Second,
            Third,
            Fourth,
            Fifth,
            Sixth,
            Seventh,
            Eight,
            Ninth,
            Tenth,
        }

        //Section List if application use in schools
        public enum Sections
        {
            A,
            B,
            C,
            D,
        }

        //Hobbies list for student both school and college students
        public enum Hobbies
        {
            Sing,
            Cricket,
            Video_Games,
            Reading_Books,
            Cooking,
            TV,
            Social_Media,
            Others,
        }

        //Staff Designation for Schools
        public enum S_Designations
        {
            Principal,
            Teacher,
        }

        //Staff Designation for College
        public enum C_Designations
        {
            Principal,
            Vice_Principal,
            Assistant_Professor,
            Library_Staff,
        }
        

    }

    //Student Base Class
    public class Student
    {
        public int reg_no { get; set; }
        public string passcode { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public DateTime dob { get; set; }
        public string description { get; set; }
        public List<Data_model.Hobbies> hobbies { get; set; }
        public DateTime joining_date { get; set; }

    }

    //School Student Model
    [Serializable]
    public class School_student : Student
    {
        public float average_mark { get; set; }
        public string rank { get; set; }

        public string standard { get; set; }
        public string section { get; set; }

    }

    //College Student Model
    [Serializable]
    public class College_student : Student
    {
        public string department { get; set; }
        public string email_id { get; set; }
        public long phone_no { get; set; }
        public float x_mark { get; set; }
        public float xii_mark { get; set; }
        public float curr_cgpa { get; set; }
        public int total_cgpa { get; set; }


    }

    //Base Staff Class
    public class Staff
    {
        public int staff_id { get; set; }
        public string passcode { get; set; }
        public string name { get; set; }
        public string email_id { get; set; }
        public long phone_no { get; set; }
       
        public int salary { get; set; }

    }

    //Staff Options
    interface Staff_operations
    {
        void addStudent();
        void removeStudent();
        void addmarkList();

    }

    //Principal Operation
    interface Principal_operation:Staff_operations
    {
        public void addStaff<T>();
        void updateSalary();
    }

    public class Staff_operation : Staff_operations, Principal_operation
    {
        void Staff_operations.addmarkList()
        {
            throw new NotImplementedException();
        }

        void Principal_operation.addStaff<T>()
        {
            
            if (Student_Project.appInfo.org_type.Equals("School"))
            {
                School_staff st = new School_staff();

                Console.WriteLine("Enter the Staff_id");
                st.staff_id=int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Name");
                st.name=Console.ReadLine();

                Console.WriteLine("Enter the Staff Email ID");
                st.email_id=Console.ReadLine();

                while (!Validation.isEmail(st.email_id))
                {
                    Console.WriteLine("Please Enter Valid Email");
                    st.email_id = Console.ReadLine();
                }

                Console.WriteLine("Enter the Staff Phone Number");
                st.phone_no=long.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Salary");
                st.salary=int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Designation");
                String str = "";
                int j = 1;
                foreach(var i in Enum.GetValues(typeof(Data_model.S_Designations)))
                {
                    str=str+ j.ToString()+"."+ i.ToString()+" ";
                    j++;
                }
                Console.WriteLine(str);
                int k = int.Parse(Console.ReadLine());

                st.designation = (Data_model.S_Designations)(k-1);

                Console.WriteLine("Enter the Staff Passcode");
                st.passcode=Console.ReadLine();

                Console.WriteLine("Enter the Major Subject of the Teacher");
                st.subject=Console.ReadLine();

                new Staff_List<School_staff>().addDetails(st.staff_id, st);
            }
            else
            {
                College_staff st = new College_staff();
                
            }
        }

        void Staff_operations.addStudent()
        {
            throw new NotImplementedException();
        }

        void Staff_operations.removeStudent()
        {
            throw new NotImplementedException();
        }

        void Principal_operation.updateSalary()
        {
            throw new NotImplementedException();
        }
    }

    //Student options
    interface Student_operations
    {
        void viewMark();
        void viewAllResult();
        void sendEnquiry();

    }

    //School Staff Model
    [Serializable]
    public class School_staff : Staff
    {
        public Data_model.S_Designations designation { get; set; }
        public string subject { get; set; }

        
    }

    //College Staff Model
    [Serializable]
    public class College_staff : Staff
    {
        public Data_model.C_Designations designation { get; set; }
        public string department { get; set; }
        
    }
    

    
    //Validation Class for validate the details
    public class Validation
    {
        //Check the file is available in the path
        public static bool isDataAvailable(string path)
        {
            var data = File.Exists(path);
            if (data)
            {
                return true;
            }
            
            return false;
        }

        //Validate the email
        public static bool isEmail(string email)
        {
            return false;
        }

        //Validate the Phone Number
        public static bool isPhone(string email)
        {
            return false;
        }

        //Validate the Student Login Details
        public static bool StuVerification()
        {
            return false;
        }

        //Validate the Staff Login Details
        public static bool StaffVerification()
        {
            return false ;
        }

    }

    //Data Manager for perform File Operations
    public class DatabaseManager
    {

        //Store the Application Details in the file
        public static void storeAppData(string path,AppData data,bool append=false)
        {
            using (Stream stream = File.Open(path, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }

        //Store the Student Details in the file
        public static void storeStuData()
        {

        }

        //Store the Staff Details in the file
        public static void storeStaffData()
        {

        }

        //Store the Mark Details in the file
        public static void storeMarks()
        {

        }

        //Get the Application Details
        public static AppData getAppData(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (AppData)binaryFormatter.Deserialize(stream);
            }
        }

        //Get the Student Data from the file
        public static Dictionary<int,T> getStuData<T>(string filePath)
        {
            if (Validation.isDataAvailable(filePath))
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (Dictionary<int,T>)binaryFormatter.Deserialize(stream);
                }
            }
            else
            {
                return null;
            }
            
        }

        //Get the staff Details from the file
        public static Dictionary<int, T> getStaffData<T>(string filePath)
        {
            if (Validation.isDataAvailable(filePath))
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (Dictionary<int, T>)binaryFormatter.Deserialize(stream);
                }
            }
            else
            {
                Principal_operation st = new Staff_operation();
                st.addStaff<T>();
                return null;
            }
            
        }

        //Get the Student Marks from the File
        public static void getMarks(string filePath)
        {
            if (Validation.isDataAvailable(filePath))
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    //return (AppData)binaryFormatter.Deserialize(stream);
                }
            }
            else
            {
                //return null;
            }
            
        }

    }

    //Main Project Method - Starting Point of Project
    public class Student_Project
    {
        public static AppData appInfo;
        public static int authType;
        public static bool login = false;
        public static Staff staffData = null;
        public static Student stuData = null;
        public string stu_path = "F:\\data\\student.txt";
        public string staff_path = "F:\\data\\staff.txt";

        public static void Main(string[] args)
        {
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

                DatabaseManager.storeAppData(path, appData);

            }
            //showOption();
            Console.WriteLine(String.Format("Welcome To Our Organization"));
            Console.WriteLine(String.Format("{0,-5} {1,-15}", " ",appInfo.org_name));
            
            if (appInfo.org_type == "School")
            {
                staffData=new School_staff();
                stuData = new School_student();
                getAllData<School_staff, School_student>();
                showSchlOptions();
            }
            else
            {
                
                staffData = new College_staff();
                stuData = new College_student();
                getAllData<College_staff, College_student>();
                showClgOption();
            }
            Console.Read();

        }

        //Get the Data Previously Stored
        public static void getAllData<T, T1>()
        {
            Stud_List<T>.getInstance().getStudList();
            DatabaseManager.getStaffData<T>(new Student_Project().staff_path);
        }

        //Show the Options for Login
        public static void showClgOption()
        {

            Console.WriteLine("Select option\n1.Staff Login\n2.Student Login\n3.Exit");
            try
            {
                authType = int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please Enter valid input" + e);
            }

        }

        //Show the options for login
        public static void showSchlOptions()
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
                    throw new Exception("Program Exited");
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

        }

        //Verify the Student Details
        public static void studentLogin()
        {

        }
    }
}
