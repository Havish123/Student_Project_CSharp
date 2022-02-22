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
        public List<Data_model.Hobbies> hobbies { get; set; }
        public DateTime joining_date { get; set; }

    }

    //School Student Model
    [Serializable]
    public class School_student : Student
    {
        public float average_mark { get; set; }

        public Data_model.Standard standard { get; set; }
        public Data_model.Sections section { get; set; }

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

    }



}
