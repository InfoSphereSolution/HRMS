using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AccessModel
    {
        public Int32 UserID { get; set; }
        public Int32 DesignationID { get; set; }
        public Int32 MenuID { get; set; }
        public Boolean IsAdd { get; set; }
        public Boolean IsUpdate { get; set; }
        public Boolean IsDelete { get; set; }
        public Boolean IsApprove { get; set; }
    }
}

