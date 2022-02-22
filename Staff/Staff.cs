﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    //Base Staff Class
    [Serializable]
    public class Staff
    {
        public Staff()
        {

        }
        public int staff_id { get; set; }
        public string passcode { get; set; }
        public string name { get; set; }
        public string email_id { get; set; }
        public long phone_no { get; set; }

        public int salary { get; set; }

    }

    //School Staff Model
    [Serializable]
    public class School_staff : Staff
    {
        public School_staff()
        {

        }
        public Data_model.S_Designations designation { get; set; }
        public string subject { get; set; }


    }

    //College Staff Model
    [Serializable]
    public class College_staff : Staff
    {
        public College_staff()
        {

        }
        public Data_model.C_Designations designation { get; set; }
        public string department { get; set; }

    }

    //Staff Options
    interface Staff_operations
    {
        void addStudent();
        void removeStudent();
        void addmarkList();

    }

    //Principal Operation
    interface Principal_operation : Staff_operations
    {
        public void addStaff();
        void updateSalary();
        void showStaffs();
    }

    public class Staff_operation : Staff_operations, Principal_operation
    {
        public void showStaffs()
        {
            var staffList = Staff_List.getInstance().getStaffList();
            foreach ((int id, School_staff staff) in staffList)
            {
                Console.WriteLine(staff.staff_id + " " + staff.name + " " + staff.email_id + " " + staff.phone_no + " " + staff.subject + " " + staff.designation);
            }
        }

        void Staff_operations.addmarkList()
        {
            throw new NotImplementedException();
        }

        void Principal_operation.addStaff()
        {

            if (Student_Project_Main.appInfo.org_type.Equals("School"))
            {
                School_staff st = new School_staff();

                Console.WriteLine("Enter the Staff_id");
                st.staff_id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Name");
                st.name = Console.ReadLine();

                Console.WriteLine("Enter the Staff Email ID");
                st.email_id = Console.ReadLine();

                while (!Validation.isEmail(st.email_id))
                {
                    Console.WriteLine("Please Enter Valid Email");
                    st.email_id = Console.ReadLine();
                }

                Console.WriteLine("Enter the Staff Phone Number");
                st.phone_no = long.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Salary");
                st.salary = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the Staff Designation");
                String str = "";
                int j = 1;
                foreach (var i in Enum.GetValues(typeof(Data_model.S_Designations)))
                {
                    str = str + j.ToString() + "." + i.ToString() + " ";
                    j++;
                }
                Console.WriteLine(str);
                int k = int.Parse(Console.ReadLine());

                st.designation = (Data_model.S_Designations)(k - 1);

                Console.WriteLine("Enter the Staff Passcode");
                st.passcode = Console.ReadLine();

                Console.WriteLine("Enter the Major Subject of the Teacher");
                st.subject = Console.ReadLine();

                Staff_List.getInstance().addDetails(st.staff_id, st);
                DatabaseManager.storeStaffData();
                //new Staff_List<School_staff>().addDetails(st.staff_id, st);
            }
            else
            {
                College_staff st = new College_staff();

            }
        }

        void Staff_operations.addStudent()
        {
            School_student st = new School_student();

            Console.WriteLine("Enter the Student Register Number");
            st.reg_no = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Student FirstName");
            st.f_name = Console.ReadLine();
            Console.WriteLine("Enter the Student Last Name");
            st.l_name = Console.ReadLine();
            Console.WriteLine("Enter the Student Date of Birth");
            st.dob = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Student Description");
            st.description = Console.ReadLine();

            string str = "";
            int j = 1;
            foreach (var i in Enum.GetValues(typeof(Data_model.Standard)))
            {
                str = str + j.ToString() + "." + i.ToString() + " ";
                j++;
            }
            Console.WriteLine(str + " " + j + ".Exit Enter the Student Class");
            int k=int.Parse(Console.ReadLine());
            st.standard = (Data_model.Standard)(k - 1);

            str = "";
            j = 1;
            foreach (var i in Enum.GetValues(typeof(Data_model.Sections)))
            {
                str = str + j.ToString() + "." + i.ToString() + " ";
                j++;
            }
            Console.WriteLine(str + " " + j + ".Exit Enter the student Section");
            k = int.Parse(Console.ReadLine());
            st.section = (Data_model.Sections)(k - 1);

            str = "";
            j = 1;
            foreach (var i in Enum.GetValues(typeof(Data_model.Hobbies)))
            {
                str = str + j.ToString() + "." + i.ToString() + " ";
                j++;
            }
            Console.WriteLine(str + " " + j + ".Exit Enter your hobies");
            while (true)
            {
                k = int.Parse(Console.ReadLine());

                if (k == j && st.hobbies.Any())
                {
                    break;
                }
                else if (k == j)
                {
                    Console.WriteLine("Please Enter Atleast one hobbies");
                }
                else
                {
                    st.hobbies.Add((Data_model.Hobbies)(k - 1));
                }
            }
            Console.WriteLine("Enter the Student Passcode");
            st.passcode = Console.ReadLine();
            
            st.average_mark = 0;
            st.joining_date = DateTime.Today;

            Stud_List.getInstance().addDetails(st.standard, st);
            DatabaseManager.storeStuData();
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
}