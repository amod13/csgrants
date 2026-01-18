using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrantApp.Models
{
    public class VDCMUNVIewModel
    {
        public int ProvinceId { get; set; }
        public int DistrictCode { get; set; }
        public int VDCMUNCode { get; set; }
        public string OldVDCName { get; set; }
        public string NewVDCName { get; set; }
        
    }
}