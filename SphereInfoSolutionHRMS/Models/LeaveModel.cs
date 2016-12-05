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
        public int LeaveID { get; set; }
        public int LeaveType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public String Contact { get; set; }
        public String Reason { get; set; }
        public Boolean IsHalfDay { get; set; }        
    }
}
