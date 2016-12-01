using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClientModel
    {
        public int tempclientId { get; set; }
        public int CreatedBy { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string ClientAddress { get; set; }
        public int pinCode { get; set; }
        public string WebSite { get; set; }
        public string ContactNo { get; set; }
        public string IP_Address { get; set; }
        public bool Is_Sat_Working { get; set; }
        public int AddedBy { get; set; }
        public int General_Shift1 { get; set; }
        public int General_Shift2 { get; set; }
        public int General_Shift3 { get; set; }
        public int FirstShift { get; set; }
        public int SecondShift { get; set; }
        public int NightShift { get; set; }
        public bool IsCustomShift { get; set; }
        public int FlexibleTime { get; set; }
        public int Operation { get; set; }
        public int paidholiday { get; set; }
        public bool sat1 { get; set; }
        public bool sat2 { get; set; }
        public bool sat3 { get; set; }
        public bool sat4 { get; set; }
        public bool sat5 { get; set; }
        public int optionalholiday { get; set; }
        public string ShipName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Hours { get; set; }
        public int UpdatedBy { get; set; }

    }
}
