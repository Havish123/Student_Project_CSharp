using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
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
            return true;
        }

        //Validate the Phone Number
        public static bool isPhone(string email)
        {
            return false;
        }

        //Validate the Student Login Details
        public static bool StuVerification(int id, string pass)
        {
            var stData = Stud_List.getInstance().getStudList();
            bool flag = true;
            foreach ((var sd, var stList) in stData)
            {
                if (stList.ContainsKey(id))
                {
                    if (stList[id].passcode == pass)
                    {
                        Console.WriteLine("Verification Success");
                        Student_Project_Main.login = true;
                        return true;
                    }

                }
                else
                {
                    continue;
                }
            }

            Console.WriteLine("No Student Data found... \nCheck your Details or Contact your Staff...");

            return false;
        }

        //Validate the Staff Login Details
        public static bool StaffVerification(int id, string pass)
        {
            var staffData = Staff_List.getInstance().getStaffList();

            if (!staffData.ContainsKey(id))
            {
                Console.WriteLine("The Staff Data Not Found.....Invalid Id......");
                return false;
            }
            else if (pass.Equals(staffData[id].passcode))
            {
                Student_Project_Main.staffData = staffData[id];
                Console.WriteLine("Verification Success");
                Student_Project_Main.login = true;
                return true;
            }
            Console.WriteLine("Invalid Password...");
            return false;
        }

    }

}
