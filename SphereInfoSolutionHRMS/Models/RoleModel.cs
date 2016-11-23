using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public  class RoleModel
    {
        public string RoleName { get; set; }
        public int RoleLevel { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int RoleId { get; set; }
        public int Operation { get; set; }
        public int TempRoleId { get; set; }
       
    }
}
