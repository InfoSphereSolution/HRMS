using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Models
{
    public class LeaveModel
    {
        public Int32 UserID { get; set; }
        public Int32 LeaveID { get; set; }
        public Int32 LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public String Contact { get; set; }
        public String Reason { get; set; }
        public String LeaveStatus { get; set; }
        public Int32 UpdatedBy { get; set; }          
    }
}
