using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Models
{
    public class ReportModel
    {        
        public Int32 ClientID { get; set; }
        public Int32 UserID { get; set; }
        public Int32 EmployeeID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Int32 ReportType { get; set; }        
    }
}

