using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
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
            First,
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
}
