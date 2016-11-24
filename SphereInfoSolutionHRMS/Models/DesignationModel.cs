using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DesignationModel
    {
        public int DesigId { get; set; }
        public string DesignationName { get; set; }
        public int RoleId { get; set; }
        public int DeptId { get; set; }
        public int CreatedBy { get; set; }
        public int Operation { get; set; }
        public int TempDesigId { get; set; }
        public int UpdatedBy { get; set; }

    }
}