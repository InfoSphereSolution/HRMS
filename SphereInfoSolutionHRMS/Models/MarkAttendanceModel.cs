using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MarkAttendanceModel
    {
        public Int32 UserID { get; set; }
        public String IPAddress { get; set; }

        //To fetch attendance between FromDate and ToDate
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
