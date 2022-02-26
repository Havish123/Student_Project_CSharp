using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    //Student Base Class
    [Serializable]
    public class Student
    {
        public int reg_no { get; set; }
        public string passcode { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public DateTime dob { get; set; }
        public string description { get; set; }
        public List<Data_model.Hobbies> hobbies = new List<Data_model.Hobbies>();
        public DateTime joining_date { get; set; }

        public string getHobbies()
        {
            string str = "";
            foreach (var i in hobbies)
            {
                str += i.ToString() + " ";
            }
            return str;
        }


    }

    //School Student Model
    [Serializable]
    public class School_student : Student
    {
        public float average_mark { get; set; }

        public Data_model.Standard standard { get; set; }
        public Data_model.Sections section { get; set; }

        public Data_model.Standard getStandard()
        {
            return (Data_model.Standard)standard;
        }

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


    //Student options
    interface Student_operations
    {
        void viewMark();
        void viewAllResult();
        void sendEnquiry();
        void viewDetails(int id, string pass);
    }

    class Stud_Operations : Student_operations
    {
        void Student_operations.sendEnquiry()
        {
            throw new NotImplementedException();
        }

        void Student_operations.viewAllResult()
        {
            throw new NotImplementedException();
        }

        void Student_operations.viewDetails(int id, string pass)
        {
            var list = Stud_List.getInstance().getStudList();
            bool flag = true;
            foreach ((var sd, var stList) in list)
            {
                if (stList.ContainsKey(id))
                {
                    if (stList[id].passcode == pass)
                    {
                        var st = stList[id];
                        Console.WriteLine("Register Number: " + st.reg_no + "\nFirst Name: " + st.f_name + "\nLast Name: " + st.l_name + "\nDate of Birth: " + st.dob
                            + "\nStudent Description: " + st.description + "\nStandard and Section: " + st.getStandard() + " " + st.section + "\nJoining Date: " + st.joining_date + "\nHobbies: " + st.getHobbies());
                    }

                }

            }
        }

        void Student_operations.viewMark()
        {
            throw new NotImplementedException();
        }
    }



}
