using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    //Data Manager for perform File Operations
    public class DatabaseManager
    {

        //Store the Application Details in the file
        public static void storeAppData(string path, AppData data, bool append = false)
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
            bool append = false;
            string path = Student_Project_Main.stu_path;
            var data = Stud_List.getInstance().getStudList();
            using (Stream stream = File.Open(path, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }

        //Store the Staff Details in the file
        public static void storeStaffData()
        {
            bool append = false;
            string path = Student_Project_Main.staff_path;
            var data = Staff_List.getInstance().getStaffList();
            using (Stream stream = File.Open(path, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
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
        public static Dictionary<Data_model.Standard, Dictionary<int, School_student>> getStuData(string filePath)
        {
            if (Validation.isDataAvailable(filePath))
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (Dictionary<Data_model.Standard, Dictionary<int, School_student>>)binaryFormatter.Deserialize(stream);
                }
            }
            else
            {
                if(Student_Project_Main.authType == 1)
                {
                    Console.WriteLine("No student Data found");
                    //Staff_operations st=new Staff_operation();
                    //st.addStudent();
                }
                else
                {
                    Console.WriteLine("No student Data found");
                }
                
                Console.WriteLine("No Student Data Available! Please Contact your Staff");
                return null;
            }

        }

        //Get the staff Details from the file
        public static Dictionary<int, School_staff> getStaffData(string filePath)
        {
            if (Validation.isDataAvailable(filePath))
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (Dictionary<int, School_staff>)binaryFormatter.Deserialize(stream);
                }
            }
            else
            {
                Console.WriteLine("No Staff Details found");
                Principal_operation st = new Staff_operation();
                st.addStaff();
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
}
