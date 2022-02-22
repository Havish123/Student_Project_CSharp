using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    //Serializable Model for Application Details
    [Serializable]
    public class AppData
    {
        public int org_id { get; set; }
        public string org_type { get; set; }
        public string org_name { get; set; }

    }
}
